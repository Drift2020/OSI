 using OSI_Net.Code;
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
            set { path_in_internet = value; OnPropertyChanged(nameof(Path_in_internet)); }
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


               list_file = my_db.Info_file.Select(x => x).ToList();
          
            OnPropertyChanged(nameof(List_file));
        }



        private async void Download()
        {
            cts = new CancellationTokenSource();
            await Task.Run(() => {
                try
                {
                    // URI, определяющий интернет-ресурс 
                    // URI (англ. Uniform Resource Identifier) — единообразный идентификатор ресурса
                    string h = path_in_internet;
                    string fileName = Path.GetFileName(path_in_internet);

                    fileName = fileName.Split(new char[] { '?' })[0];


                    HttpWebRequest request = (HttpWebRequest)HttpWebRequest.Create(h /* URI, определяющий интернет-ресурс */);

                    // Получим ответ на интернет-запрос
                    HttpWebResponse response = (HttpWebResponse)request.GetResponse();
                    // Возвращаем поток данных из полученного интернет-ресурса.
                    using (Stream stream = response.GetResponseStream())
                    {
                        long full_size = response.ContentLength;
                        byte[] b = new byte[0];// = new byte[full_size];
                        int i;

                        Counter my_count = new Counter();


                        long tmp_full_size = full_size;
                        int poz = 0;


                        #region  создание и запуск потоков

                        Info_file tmp = new Info_file();
                        tmp.Date = DateTime.Now;
                        tmp.Name = fileName;
                        tmp.Path_Net = path_in_internet;
                        tmp.Path_PC = path_for_file + fileName;
                        tmp.Status = 1;
                        my_db.Info_file.Add(tmp);
                        my_db.SaveChanges();

                        tmp.Info_fileId = my_db.Info_file.ToList().Last().Info_fileId;

                        uiContext.Send(d =>   list_file.Add(tmp), null);
                        OnPropertyChanged(nameof(List_file));

                        my_count = new Counter(0, Convert.ToInt32(tmp_full_size), poz, true, new byte[tmp_full_size], stream, tmp);
                        Thread myThread = new Thread(new ParameterizedThreadStart(Count));
                        myThread.Start(my_count);
                        #endregion
                        #region проверка активности потоков
                        bool is_Next = false;
                        do
                        {
                            is_Next = false;

                            if (myThread.IsAlive)
                                is_Next = true;

                        } while (is_Next);
                        #endregion
                        //запись в один байтовый масив                      
                        b = my_count._b.ToArray();

                        
                        #region преобразование в файл
                        FileStream st = new FileStream(path_for_file + fileName, FileMode.OpenOrCreate);
                        using (BinaryWriter writer = new BinaryWriter(st))
                        {
                            writer.Write(b);

                            writer.Close();
                         
                            var tmp1 = list_file.Where(x => x.Info_fileId == tmp.Info_fileId).ToList()[0].Status=4;
                                  
                            list_file.Where(x => x.Info_fileId == tmp.Info_fileId).ToList()[0].Status_Now = "100";
                            OnPropertyChanged(nameof(List_file));
                             

                        }
                        #endregion 
                    }
                }
                catch (Exception ex)
                {
                    System.Windows.MessageBox.Show(ex.Message);
                }
            });


        }

    

        public static void Count(object x)
        {
            Counter n = x as Counter;
            
            for (int i=0,c=0; (c = n._stream.ReadByte()) != -1;i++)
            {
                n._b[i] = (byte)c;
                n._my_file.Status_Now = (i!=0?(String.Format("{0:C2}", ((double)i / n._count*100))):"0");
                my_this. OnPropertyChanged(nameof(List_file));
              
            }
                        //  n._stream.ReadAsync(n._b, n._poz, n._count);
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

        }
        private bool CanExecute_delete(object o)
        {
            return true;
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
