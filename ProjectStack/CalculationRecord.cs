namespace ProjectStack
{
    public class CalculationRecord
    {
        public string Expression { get; set; } // Biểu thức
        public string Result { get; set; }    // Kết quả
        public DateTime Time { get; set; }    // Thời gian thực hiện

        public override string ToString()
        {
            string formattedExpression = $"{Expression ?? "N/A"} = {Result ?? "N/A"}";
            string formattedDate = Time.ToString("MM/dd/yyyy HH:mm:ss");
            return formattedExpression.PadRight(20) + formattedDate.PadLeft(30);
        }

    }

}
