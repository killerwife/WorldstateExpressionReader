using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WorldstateExpressionReader
{
    enum WorldStateExpressionFunctions
    {
        WSE_FUNCTION_NONE = 0,
        WSE_FUNCTION_RANDOM,
        WSE_FUNCTION_MONTH,
        WSE_FUNCTION_DAY,
        WSE_FUNCTION_TIME_OF_DAY,
        WSE_FUNCTION_REGION,
        WSE_FUNCTION_CLOCK_HOUR,
        WSE_FUNCTION_OLD_DIFFICULTY_ID,
        WSE_FUNCTION_HOLIDAY_START,
        WSE_FUNCTION_HOLIDAY_LEFT,
        WSE_FUNCTION_HOLIDAY_ACTIVE,
        WSE_FUNCTION_TIMER_CURRENT_TIME,
        WSE_FUNCTION_WEEK_NUMBER,
        WSE_FUNCTION_UNK13,
        WSE_FUNCTION_UNK14,
        WSE_FUNCTION_DIFFICULTY_ID,
        WSE_FUNCTION_WAR_MODE_ACTIVE,
        WSE_FUNCTION_UNK17,
        WSE_FUNCTION_UNK18,
        WSE_FUNCTION_UNK19,
        WSE_FUNCTION_UNK20,
        WSE_FUNCTION_UNK21,
        WSE_FUNCTION_WORLD_STATE_EXPRESSION,
        WSE_FUNCTION_KEYSTONE_AFFIX,
        WSE_FUNCTION_UNK24,
        WSE_FUNCTION_UNK25,
        WSE_FUNCTION_UNK26,
        WSE_FUNCTION_UNK27,
        WSE_FUNCTION_KEYSTONE_LEVEL,
        WSE_FUNCTION_UNK29,
        WSE_FUNCTION_UNK30,
        WSE_FUNCTION_UNK31,
        WSE_FUNCTION_UNK32,
        WSE_FUNCTION_MERSENNE_RANDOM,
        WSE_FUNCTION_UNK34,
        WSE_FUNCTION_UNK35,
        WSE_FUNCTION_UNK36,
        WSE_FUNCTION_UI_WIDGET_DATA,
        WSE_FUNCTION_TIME_EVENT_PASSED,

        WSE_FUNCTION_MAX,
    };

    public class ExpressionReader
    {
        public static List<byte> HexToByte(string expression)
        {
            List<byte> byteArray = new List<byte>();
            for (int i = 0; i < expression.Length; i = i + 2)
            {
                byte number = Convert.ToByte(expression.Substring(i, 2), 16);
                byteArray.Add(number);
            }
            return byteArray;
        }

        public static async Task ReadWorldstateExpression(BinaryReader reader, IDbContext context)
        {
            bool enabled = reader.ReadBoolean();

            // Console.Write("Expr: " + (enabled ? "enabled" : "disabled") + " ");

            await ReadRelOp(reader, context);

            while (reader.BaseStream.Position != reader.BaseStream.Length)
            {
                byte logic = reader.ReadByte();
                switch (logic)
                {
                    case 0: return;
                    case 1: Console.Write("&&" + " "); break;
                    case 2: Console.Write("||" + " "); break;
                    case 3: Console.Write("!=" + " "); break;
                }
                await ReadRelOp(reader, context);
            }
        }

        public static async Task ReadFunction(BinaryReader reader, IDbContext context, UInt32 functionType)
        {
            var arg1 = await ReadSingleVal(reader, context);
            var arg2 = await ReadSingleVal(reader, context);

            switch ((WorldStateExpressionFunctions)functionType)
            {
                case WorldStateExpressionFunctions.WSE_FUNCTION_NONE: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_RANDOM: Console.Write("Random"); break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_MONTH: Console.Write("Month"); break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_DAY: Console.Write("Day"); break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_TIME_OF_DAY: Console.Write("Minutes"); break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_REGION: Console.Write("Region"); break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_CLOCK_HOUR: Console.Write("Hour"); break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_OLD_DIFFICULTY_ID: Console.Write("Old difficulty id"); break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_HOLIDAY_START: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_HOLIDAY_LEFT: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_HOLIDAY_ACTIVE: Console.Write("Active holiday"); break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_TIMER_CURRENT_TIME: Console.Write("Current time"); break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_WEEK_NUMBER: Console.Write("Week number"); break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK13: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK14: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_DIFFICULTY_ID: Console.Write("Difficulty id"); break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_WAR_MODE_ACTIVE: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK17: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK18: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK19: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK20: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK21: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_WORLD_STATE_EXPRESSION:
                    Console.Write("Expression");
                    var recursiveExpression = await context.GetWorldstateExpression(arg1);
                    var byteArray = HexToByte(recursiveExpression!);

                    MemoryStream stream = new MemoryStream(byteArray.ToArray());
                    using (BinaryReader recursiveReader = new BinaryReader(stream))
                    {
                        await ReadWorldstateExpression(recursiveReader, context);
                    }
                    break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_KEYSTONE_AFFIX: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK24: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK25: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK26: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK27: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_KEYSTONE_LEVEL: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK29: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK30: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK31: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK32: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_MERSENNE_RANDOM: Console.Write("MERSENNE_RANDOM"); break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK34: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK35: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UNK36: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_UI_WIDGET_DATA: break;
                case WorldStateExpressionFunctions.WSE_FUNCTION_TIME_EVENT_PASSED: break;
            }
        }

        public static async Task<Int32> ReadSingleVal(BinaryReader reader, IDbContext context)
        {
            byte valueType = reader.ReadByte();

            switch (valueType)
            {
                case 0: break;
                case 1:
                    Int32 constant = reader.ReadInt32();
                    Console.Write(constant + " ");
                    return constant;
                case 2:
                    Int32 variableId = reader.ReadInt32();
                    string? variableName = await context.GetWorldstateName(variableId);
                    Console.Write("WorldState(" + variableId + " "+ (variableName != null ? variableName : "") + ") "); // TODO: Expand id to name
                    return variableId;
                case 3:
                    UInt32 functionType = reader.ReadUInt32();
                    Console.Write("Function type: " + functionType + " ");
                    Console.Write("( ");
                    await ReadFunction(reader, context, functionType);
                    Console.Write(") ");
                    break;
            }

            return 0;
        }

        public static async Task ReadVal(BinaryReader reader, IDbContext context)
        {
            await ReadSingleVal(reader, context);

            byte operatorType = reader.ReadByte();

            switch (operatorType)
            {
                case 0:
                    // Console.WriteLine("None");
                    break;
                case 1:
                    Console.Write("+" + " ");
                    break;
                case 2:
                    Console.Write("-" + " ");
                    break;
                case 3:
                    Console.Write("*" + " ");
                    break;
                case 4:
                    Console.Write("/" + " ");
                    break;
                case 5:
                    Console.Write("%" + " ");
                    break;
            }

            if (operatorType == 0)
                return;

            await ReadSingleVal(reader, context);
        }

        public static async Task ReadRelOp(BinaryReader reader, IDbContext context)
        {
            await ReadVal(reader, context);

            byte conditionOperation = reader.ReadByte();

            switch (conditionOperation)
            {
                case 0: /*Console.Write("NONE" + " ");*/ return;
                case 1: Console.Write("==" + " "); break;
                case 2: Console.Write("!=" + " "); break;
                case 3: Console.Write("<" + " "); break;
                case 4: Console.Write("<=" + " "); break;
                case 5: Console.Write(">" + " "); break;
                case 6: Console.Write(">=" + " "); break;
            }

            await ReadVal(reader, context);
        }
    }
}
