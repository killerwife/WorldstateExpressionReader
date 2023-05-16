// See https://aka.ms/new-console-template for more information
using WorldstateExpressionReader;
using WorldstateExpressionReader.Repositories;

bool cmangos = true;

IDbContext context;
if (cmangos)
    context = new CmangosContext("server=localhost; database=tbcmangos; user=root; password=deadlydeath");
else
    context = new TCContext("server=localhost; database=tdb; user=root; password=deadlydeath");

var expressions = await context.GetAllExpressions();

foreach (var expression in expressions!)
{
    var byteArray = ExpressionReader.HexToByte(expression.Expression);
    Console.Write("Id: " + expression.Id + " ");
    MemoryStream stream = new MemoryStream(byteArray.ToArray());
    using (BinaryReader reader = new BinaryReader(stream))
    {
        await ExpressionReader.ReadWorldstateExpression(reader, context);
    }
    Console.WriteLine();
}
