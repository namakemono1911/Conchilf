namespace WiimoteApi
{
    public class ButtonData : WiimoteData
    {

        private bool[] isPress = new bool[11];

        public bool[] Data
        {
            get { return isPress; }
        }

        public ButtonData(Wiimote Owner) : base(Owner)
        {
            for (int i = 0; i < 11; i++)
                isPress[i] = false;
        }

        public override bool InterpretData(byte[] data)
        {
            if (data == null || data.Length != 2) return false;

            isPress[(int)WiiButtonCode.left] = (data[0] & 0x01) == 0x01;
            isPress[(int)WiiButtonCode.right] = (data[0] & 0x02) == 0x02;
            isPress[(int)WiiButtonCode.down] = (data[0] & 0x04) == 0x04;
            isPress[(int)WiiButtonCode.up] = (data[0] & 0x08) == 0x08;
            isPress[(int)WiiButtonCode.plus] = (data[0] & 0x10) == 0x10;

            isPress[(int)WiiButtonCode.two] = (data[1] & 0x01) == 0x01;
            isPress[(int)WiiButtonCode.one] = (data[1] & 0x02) == 0x02;
            isPress[(int)WiiButtonCode.b] = (data[1] & 0x04) == 0x04;
            isPress[(int)WiiButtonCode.a] = (data[1] & 0x08) == 0x08;
            isPress[(int)WiiButtonCode.minus] = (data[1] & 0x10) == 0x10;

            isPress[(int)WiiButtonCode.home] = (data[1] & 0x80) == 0x80;

            return true;
        }

    }

    //ボタン番号割り当て
    public enum WiiButtonCode
    {
        a = 0,
        b = 1,
        one = 2,
        two = 3,
        up,
        down,
        right,
        left,
        plus,
        minus,
        home
    }
}