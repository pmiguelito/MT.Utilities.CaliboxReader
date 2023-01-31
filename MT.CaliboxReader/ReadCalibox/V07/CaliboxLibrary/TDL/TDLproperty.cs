using MT.OneWire;

namespace CaliboxLibrary
{
    /**********************************************
     * Description: DataBase Properties
     * 
     * *******************************************/
    public class TDLproperty
    {
        public MT.OneWire.TDL_Property TDL_Property { get; private set; } = new TDL_Property();
        /***************************************
         * main info
        '***************************************/
        public int page_no
        {
            get { return TDL_Property.PageNo; }
            set { TDL_Property.PageNo = value; }
        }
        public int order_no { get; set; }
        public string property_tag
        {
            get { return TDL_Property.Property; }
            set { TDL_Property.Property = value; }
        }
        public string description
        {
            get { return TDL_Property.Description; }
            set { TDL_Property.Description = value; }
        }

        /***************************************
         * data
        '***************************************/
        public int bit
        {
            get { return TDL_Property.Bit; }
            set { TDL_Property.Bit = value; }
        }
        public int bit_start

        {
            get { return TDL_Property.BitStart; }
            set { TDL_Property.BitStart = value; }
        }
        public string data_type
        {
            get { return TDL_Property.DataType; }
            set { TDL_Property.DataType = value; }
        }

        /***************************************
         * calculation factors
        '***************************************/
        public string start
        {
            get { return TDL_Property.Start; }
            set { TDL_Property.Start = value; }
        }
        public string tol
        {
            get { return TDL_Property.Tol; }
            set { TDL_Property.Tol = value; }
        }
        public string format
        {
            get { return TDL_Property.Format; }
            set { TDL_Property.Format = value; }
        }
        public string unit
        {
            get { return TDL_Property.PhysicalUnit; }
            set { TDL_Property.PhysicalUnit = value; }
        }

        public string value
        {
            get { return TDL_Property.Value; }
            set { TDL_Property.Value = value; }
        }
    }
}
