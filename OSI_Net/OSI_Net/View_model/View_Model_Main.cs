﻿ using OSI_Net.Code;
using OSI_Net.Model;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mime;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Forms;
using System.Windows.Input;

namespace OSI_Net.View_model
{
    class View_Model_Main : View_Model_Base
    {
        //символы для валидации
        char[] sumbls = { '?', '&' };



        #region variables

        #region path_for_file
        string path_for_file = "";
        public string Path_for_file { set { path_for_file = value; OnPropertyChanged(nameof(Path_for_file)); }
            get { return path_for_file; }
        }
        #endregion path_for_file

        #region path_in_internet
        string path_in_internet = "";
        public string Path_in_internet
        {
            set { path_in_internet = value;
                try
                {
                    string fileName = Path.GetFileName(path_in_internet);
                    Name = fileName;
                    foreach (var tmp in sumbls)
                    {
                        if (fileName.IndexOf(tmp) !=-1)
                        {
                            Name = fileName.Split(tmp)[0];

                        }
                    }
               

                }
                catch
                {

                }
                OnPropertyChanged(nameof(Path_in_internet)); }
            get { return path_in_internet; }
        }
        #endregion path_in_internet

        #region UpDown
        private int _numValue = 1;

        public string NumValue
        {
            get { return _numValue.ToString(); }
            set
            {


                _numValue = Convert.ToInt32(value);
                OnPropertyChanged(nameof(NumValue));

            }
        }


        #region UP

        private DelegateCommand _Command_up_product;
        public ICommand Button_up_product
        {
            get
            {
                if (_Command_up_product == null)
                {
                    _Command_up_product = new DelegateCommand(Execute_up_product, CanExecute_up_product);
                }
                return _Command_up_product;
            }
        }
        private void Execute_up_product(object o)
        {
            _numValue += 1;
            OnPropertyChanged(nameof(NumValue));
        }
        private bool CanExecute_up_product(object o)
        {

            return true;



        }

        #endregion

        #region DOWN

        private DelegateCommand _Command_down_product;
        public ICommand Button_down_product
        {
            get
            {
                if (_Command_down_product == null)
                {
                    _Command_down_product = new DelegateCommand(Execute_down_product, CanExecute_down_product);
                }
                return _Command_down_product;
            }
        }
        private void Execute_down_product(object o)
        {
            _numValue -= 1;
            OnPropertyChanged(nameof(NumValue));
        }
        private bool CanExecute_down_product(object o)
        {
            if (_numValue > 1)
                return true;

            return false;

        }

        #endregion DOWN

        #endregion UpDown

        #region name
        string name;
        public string Name
        {
            get
            {
                return name;
            }
            set
            {
                name = value;
                OnPropertyChanged(nameof(Name));
            }
        }
        #endregion name
        CancellationTokenSource cts;
        public SynchronizationContext uiContext = new SynchronizationContext();
        My_Files my_db;
        public static View_Model_Main my_this;
        #endregion variables

        #region Func
        public View_Model_Main()
        {
            my_db = new My_Files();
          
            my_this = this;
            //Info_file t = new Info_file();
            //t.Name = "ddd";
            //t.Date = DateTime.Now;
            //t.Path_Net = "Path_Net_test1";
            //t.Path_PC = "Path_PC_test1";
            //t.Status = 4;
            //my_db.Info_file.Add(t);
            //my_db.SaveChanges();
         //   my_db.Database.Delete();

            list_file = my_db.Info_file.Select(x => x).ToList();
            Found_file();
            OnPropertyChanged(nameof(List_file));
        }

        private async void Download()
        {
            cts = new CancellationTokenSource();
            var is_work= Is_copy(path_for_file + name);
            if(is_work==0|| is_work==1)
            await Task.Run(() => {
                try
                {
                  
                    string h = path_in_internet;

                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(h /* URI, определяющий интернет-ресурс */);            
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                  
                    using (Stream stream = response.GetResponseStream())
                    {
                      
                        long full_size = response.ContentLength;
                        byte[] b = new byte[0];// = new byte[full_size];
                        int i;

                        Counter my_count = new Counter();

                        long tmp_full_size = full_size;
                        int poz = 0;

                        #region  создание и запуск потоков
                        Thread myThread =null;
                        Info_file tmp = new Info_file();
                        New_Thread(tmp,out my_count, tmp_full_size, poz, stream, out myThread);
                    
                        #endregion
                        #region проверка активности потоков
                        Is_live_Thread(ref myThread);
                        #endregion
                        //запись в один байтовый масив                      
                        b = my_count._b.ToArray();


                        #region преобразование в файл
                        Add_file_on_device(b, tmp);
                        #endregion
                    }
                    response.Dispose();

                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                    
                }
            });


        }
        void Found_file()
        {
            foreach(var tmp1 in list_file)
            {
                if (!System.IO.File.Exists(tmp1.Path_PC))
                {
                    tmp1.Status = 5;
                }


            }
            OnPropertyChanged(nameof(List_file));
        }
        int Is_copy(string path_for_file)
        {
            var  tmp =  list_file.Where(x => x.Path_PC.ToLower() == path_for_file.ToLower()).ToList();
            if(tmp.Count>0)
            {
                var result = System.Windows.MessageBox.Show("Download already exists, want to replace it?", "Error",MessageBoxButton.OKCancel);
                if(result==MessageBoxResult.OK)
                {
                    list_file.Remove(tmp[0]);
                    my_db.Info_file.Remove(tmp[0]);
                    my_db.SaveChanges();
                    return 1;
                }
               else
                    return 2;
            }
            return 0;
        }
        void Add_file_on_device(byte[] b,Info_file tmp)
        {
            FileStream st = new FileStream(path_for_file + name, FileMode.OpenOrCreate);
            using (BinaryWriter writer = new BinaryWriter(st))
            {
                writer.Write(b);

                writer.Close();

                var tmp1 = list_file.Where(x => x.Info_fileId == tmp.Info_fileId).ToList()[0].Status = 4;

                list_file.Where(x => x.Info_fileId == tmp.Info_fileId).ToList()[0].Status_Now = "100";
                OnPropertyChanged(nameof(List_file));


            }
        }
        void New_Thread(Info_file tmp, out Counter my_count, long tmp_full_size,int poz, Stream stream, out Thread myThread)
        {

            tmp.Date_end = DateTime.Now;
            tmp.Date_start = DateTime.Now;
            tmp.Name = name;
            tmp.Path_Net = path_in_internet;
            tmp.Path_PC = path_for_file + name;
            tmp.Status = 1;
            my_db.Info_file.Add(tmp);
            my_db.SaveChanges();

            tmp.Info_fileId = my_db.Info_file.ToList().Last().Info_fileId;

            uiContext.Send(d => list_file.Add(tmp), null);
            OnPropertyChanged(nameof(List_file));

            my_count = new Counter(0, Convert.ToInt32(tmp_full_size), poz, true, new byte[tmp_full_size], stream, tmp);
            myThread = new Thread(new ParameterizedThreadStart(Count));
            myThread.Start(my_count);
        }
        void Is_live_Thread(ref Thread myThread)
        {
            bool is_Next = false;
            do
            {
                is_Next = false;

                if (myThread.IsAlive)
                    is_Next = true;

            } while (is_Next);
        }
        public static void Count(object x)
        {
            Counter n = x as Counter;
            
            for (int i=0,c=0; (c = n._stream.ReadByte()) != -1;i++)
            {
                n._b[i] = (byte)c;
                n._my_file.Status_Now = (i!=0?(String.Format("{0:F2}", ((double)i / n._count*100))):"0");
                my_this. OnPropertyChanged(nameof(List_file));
              
            }
                      
        }

        public class Counter : View_Model_Base
        {
            public int _number;
            public bool _isWork;
            public int _count;
            public int _poz;
            public byte[] _b;
            public Stream _stream;
            public Info_file _my_file;
           public  Counter()
            {
                _poz = _count = _number = 0;
                _stream = null;
                _isWork = true;
                _my_file = null;

            }
            public Counter(int number, int count, int poz, bool isWork, byte[] b,Stream stream, Info_file my_file)
            {
                _poz = poz;
                _count = count;
                _number = number;
                _isWork = isWork;
                _b = b;
                _stream = stream;
                _my_file = my_file;
            }
        }
        #endregion Func

        #region Command

        #region Open for file

        private DelegateCommand Command_open_for_file;
        public ICommand Button_clik_open_for_file
        {
            get
            {
                if (Command_open_for_file == null)
                {
                    Command_open_for_file = new DelegateCommand(Execute_open_for_file, CanExecute_open_for_file);
                }
                return Command_open_for_file;
            }
        }
        private void Execute_open_for_file(object o)
        {
            using (FolderBrowserDialog tmp_view = new FolderBrowserDialog())
            {
                tmp_view.ShowDialog();
                Path_for_file = tmp_view.SelectedPath;
            }
        }
        private bool CanExecute_open_for_file(object o)
        {
            return true;
        }

        #endregion Open for file
        #region delete

        private DelegateCommand Command_delete;
        public ICommand Button_clik_delete
        {
            get
            {
                if (Command_delete == null)
                {
                    Command_delete= new DelegateCommand(Execute_delete, CanExecute_delete);
                }
                return Command_delete;
            }
        }
        private void Execute_delete(object o)
        {
            var result = System.Windows.MessageBox.Show("Want to delete this item?", "Delete", MessageBoxButton.OKCancel);
            if(result==MessageBoxResult.OK)
            {
                list_file.Remove(select_elem);
                var i =  my_db.Info_file.Remove(select_elem);
                my_db.SaveChanges();
                FileInfo tmp = new FileInfo(i.Path_PC);
                if(tmp!=null)
                tmp.Delete();

                OnPropertyChanged(nameof(List_file));
                select_elem = null;
            }
        }
        private bool CanExecute_delete(object o)
        {
            if(select_elem!=null)
            return true;
            return false;
        }

        #endregion delete
        #region Rename

        private DelegateCommand Command_rename;
        public ICommand Button_clik_rename
        {
            get
            {
                if (Command_rename == null)
                {
                    Command_rename = new DelegateCommand(Execute_rename, CanExecute_rename);
                }
                return Command_rename;
            }
        }
        private void Execute_rename(object o)
        {

        }
        private bool CanExecute_rename(object o)
        {
            return true;
        }

        #endregion Rename


        #region Download

        private DelegateCommand Command_download;
        public ICommand Button_clik_download
        {
            get
            {
                if (Command_download == null)
                {
                    Command_download = new DelegateCommand(Execute_download, CanExecute_download);
                }
                return Command_download;
            }
        }
        private void Execute_download(object o)
        {
            Download();
        }
        private bool CanExecute_download(object o)
        {
            if (path_for_file != null && path_for_file.Length > 0
                && path_in_internet != null && path_in_internet.Length>0)
            return true;
            return false;
        }

        #endregion Download
        #region Edit_path

        private DelegateCommand Command_Edit_path;
        public ICommand Button_clik_Edit_path
        {
            get
            {
                if (Command_Edit_path == null)
                {
                    Command_Edit_path = new DelegateCommand(Execute_Edit_path, CanExecute_Edit_path);
                }
                return Command_Edit_path;
            }
        }
        private void Execute_Edit_path(object o)
        {
          
        }
        private bool CanExecute_Edit_path(object o)
        {
          
                return true;
          
        }

        #endregion Download
        #region Pause

        private DelegateCommand Command_pause;
        public ICommand Button_clik_pause
        {
            get
            {
                if (Command_pause == null)
                {
                    Command_pause = new DelegateCommand(Execute_pause, CanExecute_pause);
                }
                return Command_pause;
            }
        }
        private void Execute_pause(object o)
        {

        }
        private bool CanExecute_pause(object o)
        {
            return true;
        }

        #endregion Pause
        #region Stop

        private DelegateCommand Command_stop;
        public ICommand Button_clik_stop
        {
            get
            {
                if (Command_stop == null)
                {
                    Command_stop = new DelegateCommand(Execute_stop, CanExecute_stop);
                }
                return Command_stop;
            }
        }
        private void Execute_stop(object o)
        {

        }
        private bool CanExecute_stop(object o)
        {
            return true;
        }

        #endregion Pause
        #region delete Download

        private DelegateCommand Command_delete_download;
        public ICommand Button_clik_delete_download
        {
            get
            {
                if (Command_delete_download == null)
                {
                    Command_delete_download = new DelegateCommand(Execute_delete_download, CanExecute_delete_download);
                }
                return Command_delete_download;
            }
        }
        private void Execute_delete_download(object o)
        {

        }
        private bool CanExecute_delete_download(object o)
        {
            return true;
        }

        #endregion delete Download
        #endregion Command

        #region List

        List<Info_file> list_file = new List<Info_file>();
        private string defaultFileName="Temp1";

        public ObservableCollection<Info_file> List_file
        {
            set
            {
                list_file = value.ToList();
                OnPropertyChanged(nameof(List_file));
            }
            get
            {
                return new ObservableCollection<Info_file>(list_file);
            }
        }

        Info_file select_elem = null;
        public Info_file Select_elem
        {
            set
            {
                select_elem = value;
                OnPropertyChanged(nameof(Select_elem));
            }
            get
            {
                if(select_elem!=null)
                return select_elem;
                return new Info_file();
            }
        }

        #endregion List

    }
}
