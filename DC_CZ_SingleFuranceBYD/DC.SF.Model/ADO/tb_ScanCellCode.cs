using System; 
namespace DC.SF.Model
{
    //tb_ScanCellCode
    [Serializable]
    public class tb_ScanCellCode
	{
   		     
      	/// <summary>
		/// RankCode
        /// </summary>		
		private int _rankcode;
        public int RankCode
        {
            get{ return _rankcode; }
            set{ _rankcode = value; }
        }        
		/// <summary>
		/// CellCode
        /// </summary>		
		private string _cellcode;
        public string CellCode
        {
            get{ return _cellcode; }
            set{ _cellcode = value; }
        }        
		/// <summary>
		/// ScanTime
        /// </summary>		
		private DateTime _scantime;
        public DateTime ScanTime
        {
            get{ return _scantime; }
            set{ _scantime = value; }
        }        
		/// <summary>
		/// CellType
        /// </summary>		
		private int _celltype;
        public int CellType
        {
            get{ return _celltype; }
            set{ _celltype = value; }
        }        
		/// <summary>
		/// CellModelType
        /// </summary>		
		private string _cellmodeltype;
        public string CellModelType
        {
            get{ return _cellmodeltype; }
            set{ _cellmodeltype = value; }
        }        
		/// <summary>
		/// CellPackage
        /// </summary>		
		private string _cellpackage;
        public string CellPackage
        {
            get{ return _cellpackage; }
            set{ _cellpackage = value; }
        }        
		/// <summary>
		/// CarID
        /// </summary>		
		private int _carid;
        public int CarID
        {
            get{ return _carid; }
            set{ _carid = value; }
        }        
		   
	}
}

