namespace CaliboxLibrary.DB
{
    public class Evaluation<T>
    {
        public Evaluation(string name)
        {
            _Name = name;
        }

        public override string ToString()
        {
            return Name;
        }

        private string _Name;

        public string Name
        {
            get { return _Name; }
            set { _Name = value; }
        }

        private bool _Active = true;

        public bool Active
        {
            get { return _Active; }
            set { _Active = value; }
        }

        private bool _OK;

        public bool OK
        {
            get { return _OK; }
            set
            {
                _OK = value;
                _State = 2;
            }
        }

        private int _State;

        public int State
        {
            get { return _State; }
            set
            {
                SetState(value);
            }
        }

        public bool SetState(int value)
        {
            _State = value;
            if (value < 1)
            {
                _OK = Active ? false : true;
                return _OK;
            }
            _OK = value == 1 ? false : true;
            return _OK;
        }

        private T _ValueDB;

        public T ValueDB
        {
            get { return _ValueDB; }
            set { _ValueDB = value; }
        }

        private T _Value;

        public T Value
        {
            get { return _Value; }
            set { _Value = value; }
        }

        public void Reset(T resetValue)
        {
            _OK = false;
            _State = 0;
            _Value = resetValue;
        }

        public void SetValueCompaireIsEqual(T value)
        {
            _Value = value;
            if (Active)
            {
                if (value == null)
                {
                    OK = false;
                    return;
                }
                OK = value.Equals(ValueDB);
                return;
            }
            OK = true;
        }

        public void SetValueCompaireIsEqual(T value, T compareValue)
        {
            _Value = value;
            if (Active)
            {
                if (value == null)
                {
                    OK = false;
                    return;
                }
                OK = value.Equals(compareValue);
                return;
            }
            OK = true;
        }

        public void SetValueCompaireIsNotEqual(T value, T compareValue)
        {
            _Value = value;
            if (Active)
            {
                if (value == null)
                {
                    OK = false;
                    return;
                }
                OK = value.Equals(compareValue) == false;
                return;
            }
            OK = true;
        }
    }
}
