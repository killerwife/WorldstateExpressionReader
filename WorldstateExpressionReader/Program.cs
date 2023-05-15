// See https://aka.ms/new-console-template for more information
using WorldstateExpressionReader;

var expression = "0102F40700000301640000000602FA0700000302E1070000010241080000000601040000000000";

List<byte> byteArray = new List<byte>();
for (int i = 0; i < expression.Length; i = i + 2)
{
    byte number = Convert.ToByte(expression.Substring(i, 2), 16);
    byteArray.Add(number);
}

MemoryStream stream = new MemoryStream(byteArray.ToArray());
using (BinaryReader reader = new BinaryReader(stream))
{
    ExpressionReader.ReadWorldstateExpression(reader);
}
