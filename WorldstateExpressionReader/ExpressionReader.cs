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
        public static void ReadWorldstateExpression(BinaryReader reader)
        {
            bool enabled = reader.ReadBoolean();

            Console.Write("Expr: " + (enabled ? "enabled" : "disabled") + " ");

            ReadRelOp(reader);

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
                ReadRelOp(reader);
            }
        }

        public static void ReadFunction(BinaryReader reader, UInt32 functionType)
        {
            ReadSingleVal(reader);
            ReadSingleVal(reader);

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
                case WorldStateExpressionFunctions.WSE_FUNCTION_WORLD_STATE_EXPRESSION: Console.Write("Expression"); /*TODO: Need access to all of them for nesting*/ break;
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

        public static void ReadSingleVal(BinaryReader reader)
        {
            byte valueType = reader.ReadByte();

            switch (valueType)
            {
                case 0: break;
                case 1:
                    Int32 constant = reader.ReadInt32();
                    Console.Write(constant + " ");
                    break;
                case 2:
                    UInt32 variableId = reader.ReadUInt32();
                    Console.Write("WorldState(" + variableId + ") "); // TODO: Expand id to name
                    break;
                case 3:
                    UInt32 functionType = reader.ReadUInt32();
                    Console.Write("Function type: " + functionType + " ");
                    Console.Write("( ");
                    ReadFunction(reader, functionType);
                    Console.Write(") ");
                    break;
            }
        }

        public static void ReadVal(BinaryReader reader)
        {
            ExpressionReader.ReadSingleVal(reader);

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

            ExpressionReader.ReadSingleVal(reader);
        }

        public static void ReadRelOp(BinaryReader reader)
        {
            ReadVal(reader);

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

            ReadVal(reader);
        }
    }
}
