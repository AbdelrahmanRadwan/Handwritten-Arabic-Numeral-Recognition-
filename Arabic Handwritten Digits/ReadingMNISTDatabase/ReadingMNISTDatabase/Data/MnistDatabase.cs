using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Windows.Forms;

namespace ReadingMNISTDatabase
{
    public static class MyDefinations
    {
        public const double Pi = 3.14159;
        public const int SpeedOfLight = 300000; // km per sec.
        public const int g_cImageSize = 28;
        public const int g_cVectorSize = 29;
        public const uint RAND_MAX = 0x7fff;
        public const ulong ULONG_MAX = 0xffffffff;
        public const int INT_MAX = 0x7fffffff;
        public const int GAUSSIAN_FIELD_SIZE = 21;
    }

    public struct ImageFileBegining
    {
        public int nMagic;
        public int nItems;
        public int nRows;
        public int nCols;

    };
    public struct LabelFileBegining
    {
        public int nMagic;
        public int nItems;
    };
    /// <summary>
    /// Image Pattern Class
    /// </summary>
    public class ImagePattern
    {
        public byte[] pPattern = new byte[MyDefinations.g_cImageSize * MyDefinations.g_cImageSize];
        public byte nLabel;
    }
    /// <summary>
    /// MNIST Data Class (Image+Label)
    /// </summary>
    public class MNIST_Database
    {
        protected ImageFileBegining _ImageFileBegin;
        protected LabelFileBegining _LabelFileBegin;
        private uint _iNextPattern;
        protected uint _nItems;
        protected int[] _iRandomizedPatternSequence;
        protected String _MnistImageFileName;
        protected String _MnistLabelFileName;
        protected bool _bImageFileOpen;
        protected bool _bLabelFileOpen;
        protected bool _bDatabase;
        protected bool _bFromRandomizedPatternSequence;  
        protected System.IO.BinaryReader load_ImageFile_stream;
        protected System.IO.BinaryReader load_LabelFile_stream;
      
        public List<ImagePattern> m_pImagePatterns;
        public bool m_bFromRandomizedPatternSequence
        {
            get
            {
                return _bFromRandomizedPatternSequence;
            }

        }
        public bool m_bDatabaseReady
        {
          get
          {
          	return _bDatabase;
          }
        }
   
        public MNIST_Database()
        {

            _MnistImageFileName = null;
            _MnistLabelFileName = null;
            _iNextPattern = 0;
           
            _bImageFileOpen = false;
            _bLabelFileOpen = false;
            m_pImagePatterns = null;
            load_ImageFile_stream = null;
            load_LabelFile_stream = null;
            _bDatabase = false;
            _iRandomizedPatternSequence = null;
            _bFromRandomizedPatternSequence = false;
        }
        public bool LoadMinstFiles()
        {
            //clear Image Pattern List
            if(m_pImagePatterns!=null)
            {
                m_pImagePatterns.Clear();
            }
            //close files if opened
            if (_bImageFileOpen)
            {
                
                load_ImageFile_stream.Close();
                _bImageFileOpen = false;
            }
            if (_bLabelFileOpen)
            {
                load_LabelFile_stream.Close();
                _bLabelFileOpen = false;
            }
            //load Mnist Images files.
            if (!MnistImageFileHeader())
            {
                MessageBox.Show("Can not open Image file");
                _MnistImageFileName = null;
                _bImageFileOpen = false;
                _bDatabase = false;
                return false;
            }
            if (!MnistLabelFileHeader())
            {
                MessageBox.Show("Can not open label file");
                _MnistLabelFileName = null;
                _bLabelFileOpen = false;
                _bDatabase = false;
                return false;
            }
            //check the value if image file and label file have been opened successfully
            if (_LabelFileBegin.nItems != _ImageFileBegin.nItems)
            {
                MessageBox.Show("Item numbers are different");
                CloseMinstFiles();
                _bDatabase = false;
                return false;
            }
            m_pImagePatterns = new List<ImagePattern>(_ImageFileBegin.nItems);
            _iRandomizedPatternSequence = new int[_ImageFileBegin.nItems];
            for (int i = 0; i < _ImageFileBegin.nItems; i++)
            {
                byte m_nlabel;
                byte[] m_pPatternArray = new byte[MyDefinations.g_cImageSize * MyDefinations.g_cImageSize];
                ImagePattern m_ImagePattern = new ImagePattern();
                GetNextPattern(m_pPatternArray,out m_nlabel,i, true);
                m_ImagePattern.pPattern = m_pPatternArray;
                m_ImagePattern.nLabel = m_nlabel;
                m_pImagePatterns.Add(m_ImagePattern);
            }
            _bDatabase = true;
            CloseMinstFiles();
            return true;
        
          
        }
        public void CloseMinstFiles()
        {
            load_LabelFile_stream.Close();
            load_ImageFile_stream.Close();
            _bImageFileOpen = false;
            _bLabelFileOpen = false;
        }
        /// <summary>
        /// //Get MNIST Image file'header
        /// </summary>
        protected bool MnistImageFileHeader()
        {
           
            if (_bImageFileOpen == false)
            { 
                byte[] m_byte = new byte[4];
                var openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog1.Filter = "Mnist Image file (*.idx3-ubyte)|*.idx3-ubyte";
                openFileDialog1.Title = "Open Minist Image File";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    _MnistImageFileName = openFileDialog1.FileName;

                    try
                    {
                        load_ImageFile_stream = new System.IO.BinaryReader(openFileDialog1.OpenFile());
                        //Magic number 
                        load_ImageFile_stream.Read(m_byte, 0, 4);
                        Array.Reverse(m_byte, 0, 4);
                        _ImageFileBegin.nMagic = BitConverter.ToInt32(m_byte, 0);
                        //number of images 
                        load_ImageFile_stream.Read(m_byte, 0, 4);
                        //High-Endian format to Low-Endian format
                        Array.Reverse(m_byte, 0, 4);
                        _ImageFileBegin.nItems = BitConverter.ToInt32(m_byte, 0);
                        _nItems = (uint)_ImageFileBegin.nItems;
                        //number of rows 
                        load_ImageFile_stream.Read(m_byte, 0, 4);
                        Array.Reverse(m_byte, 0, 4);
                        _ImageFileBegin.nRows = BitConverter.ToInt32(m_byte, 0);
                        //number of columns 
                        load_ImageFile_stream.Read(m_byte, 0, 4);
                        Array.Reverse(m_byte, 0, 4);
                        _ImageFileBegin.nCols = BitConverter.ToInt32(m_byte, 0);
                        _bImageFileOpen = true;
                        return true;
                    }
                    catch
                    {
                        _bImageFileOpen = false;
                        return false;
                    }
                       
                }
                return false;   
                
            }
            return true;

        }
        /// <summary>
        /// Get MNIST Label file's header
        /// </summary>
        protected bool MnistLabelFileHeader()
        {
            
            if (_bLabelFileOpen == false)
            {
                byte[] m_byte = new byte[4];
                var openFileDialog1 = new System.Windows.Forms.OpenFileDialog();
                openFileDialog1.Filter = "Mnist Label file (*.idx1-ubyte)|*.idx1-ubyte";
                openFileDialog1.Title = "Open MNIST Label file";
                if (openFileDialog1.ShowDialog() == DialogResult.OK)
                {
                    try
                    {
                        _MnistLabelFileName = openFileDialog1.FileName;
                        load_LabelFile_stream = new System.IO.BinaryReader(openFileDialog1.OpenFile());
                        //Magic number 
                        load_LabelFile_stream.Read(m_byte, 0, 4);
                        Array.Reverse(m_byte, 0, 4);
                        _LabelFileBegin.nMagic = BitConverter.ToInt32(m_byte, 0);
                        //number of images 
                        load_LabelFile_stream.Read(m_byte, 0, 4);
                        //High-Endian format to Low-Endian format
                        Array.Reverse(m_byte, 0, 4);
                        _LabelFileBegin.nItems = BitConverter.ToInt32(m_byte, 0);
                        _bLabelFileOpen = true;
                        return true;
                    }
                    catch
                    {
                        _bLabelFileOpen = false;
                        return false;
                    }
                }
                return false;
                    
            }
            return true;
        }
        /// <summary>
        /// // get current pattern number
        /// </summary>
        /// <param name="bFromRandomizedPatternSequence"></param>
        /// <returns></returns>
        public int GetCurrentPatternNumber(bool bFromRandomizedPatternSequence /* =FALSE */ )
        {
            // returns the current number of the training pattern, either from the straight sequence, or from
            // the randomized sequence

            int iRet;

            if (bFromRandomizedPatternSequence == false)
            {
                iRet = (int) _iNextPattern;
            }
            else
            {
                iRet = _iRandomizedPatternSequence[_iNextPattern];
            }

            return iRet;
        }
        public int GetNextPatternNumber(bool bFromRandomizedPatternSequence /* =FALSE */ )
        {
            // returns the current number of the training pattern, either from the straight sequence, or from
            // the randomized sequence
            if (_iNextPattern < _nItems - 1)
            {
                _iNextPattern++;
            }
            else
            {
                _iNextPattern = 0;
            }
            int iRet;

            if (bFromRandomizedPatternSequence == false)
            {
                iRet = (int)_iNextPattern;
            }
            else
            {
                iRet = _iRandomizedPatternSequence[_iNextPattern];
            }

            return iRet;
        }
        public int GetRandomPatternNumber()
        {
                Random rdm = new Random();
                int patternNum = (int)(rdm.NextDouble() * (_nItems - 1));
                return patternNum;
            
        }
        public void RandomizePatternSequence()
        {
            // randomizes the order of m_iRandomizedTrainingPatternSequence, which is a UINT array
            // holding the numbers 0..59999 in random order
            //reset iNextPattern to 0
            _iNextPattern = 0;
            int ii, jj, iiMax;
                int iiTemp;

                iiMax = (int)_nItems;
                // initialize array in sequential order

                for (ii = 0; ii < iiMax; ii++)
                {
                    _iRandomizedPatternSequence[ii] = ii;
                }


                // now at each position, swap with a random position
                Random rdm = new Random();
                for (ii = 0; ii < iiMax; ii++)
                {
                    //gives a uniformly-distributed number between zero (inclusive) and one (exclusive):(uint)(rdm.Next() / (0x7fff + 1))

                    jj = (int)(rdm.NextDouble() * iiMax);

                    iiTemp = _iRandomizedPatternSequence[ii];
                    _iRandomizedPatternSequence[ii] = _iRandomizedPatternSequence[jj];
                    _iRandomizedPatternSequence[jj] = iiTemp;
                }
                _bFromRandomizedPatternSequence = true;
        }
        /// <summary>
        /// //get value of pattern
        /// </summary>
        /// <param name="iNumImage"></param>
        /// <param name="pArray"></param>
        /// <param name="pLabel"></param>
        /// <param name="bFlipGrayscale"></param>
        protected void GetPatternArrayValues(out byte pLabel,int iNumImage = 0, byte[] pArray = null,bool bFlipGrayscale = true)
        {
            ////////
            uint cCount = MyDefinations.g_cImageSize * MyDefinations.g_cImageSize;
            long fPos;
            //
            if (_bImageFileOpen != false)
            {
                if (pArray != null)
                {
                    fPos = 16 + iNumImage * cCount;  // 16 compensates for file header info
                    //load_ImageFile_stream.Read(pArray,(int)fPos,(int)cCount);
                    load_ImageFile_stream.Read(pArray, 0, (int)cCount);
                    if (bFlipGrayscale != false)
                    {
                        for (int ii = 0; ii < cCount; ++ii)
                        {
                            pArray[ii] = Convert.ToByte(255 - Convert.ToInt32(pArray[ii]));
                        }
                    }
                }
            }
            else  // no files are open: return a simple gray wedge
            {
                if (pArray != null)
                {
                    for (int ii = 0; ii < cCount; ++ii)
                    {
                        pArray[ii] = Convert.ToByte(ii * 255 / cCount);
                    }
                }
            }
            //read label
            if (_bLabelFileOpen != false)
            {
                  fPos = 8 + iNumImage;
                  byte[] m_byte = new byte[1];
                  load_LabelFile_stream.Read(m_byte, 0, 1);
                  pLabel = m_byte[0];
                    
            }
            else
            {

                pLabel = 255;
            }
        }
        protected uint GetNextPattern(byte[] pArray /* =NULL */,out byte pLabel /* =NULL */,int index, bool bFlipGrayscale /* =TRUE */ )
        {
            // returns the number of the pattern corresponding to the pattern stored in pArray
            GetPatternArrayValues(out pLabel, index, pArray, bFlipGrayscale);
            uint iRet = _iNextPattern;
            _iNextPattern++;
            if (_iNextPattern >= _nItems)
            {
                _iNextPattern = 0;
            }
            return iRet;
        }

    }
}
