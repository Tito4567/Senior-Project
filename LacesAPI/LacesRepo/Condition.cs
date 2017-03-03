using System.Collections.Generic;

namespace LacesRepo
{
    public class Condition
    {
        public enum Operators
        {
            EqualTo = 0
            , LessThan = 1
            , GreaterThan = 2
            , Like = 3
            , IsNull = 4
            , LessOrEqual = 5
            , GreaterOrEqual = 6
        }

        public List<Condition> AndConditions { get; set; }
        public string Column { get; set; }
        public int Index { get; set; }
        public bool Invert { get; set; }
        public Operators Operator { get; set; }
        public List<Condition> OrConditions { get; set; }
        public string Value { get; set; }

        public Condition Copy()
        {
            Condition newCond = new Condition();

            newCond.Column = Column;
            newCond.Invert = Invert;
            newCond.Operator = Operator;
            newCond.Value = Value;

            return newCond;
        }

        public string GetParameterName()
        {
            return "@" + Column + Index;
        }

        public override string ToString()
        {
            string result = string.Empty;

            bool compound = false;

            if ((AndConditions != null && AndConditions.Count > 0) || (OrConditions != null && OrConditions.Count > 0))
            {
                compound = true;

                result = "(";
            }

            if (Invert)
            {
                result += "NOT ";
            }

            result += Column;

            if (Operator == Operators.IsNull)
            {
                result += " IS NULL";
            }
            else
            {
                switch (Operator)
                {
                    case Operators.EqualTo:
                        result += " = ";
                        break;
                    case Operators.GreaterOrEqual:
                        result += " >= ";
                        break;
                    case Operators.GreaterThan:
                        result += " > ";
                        break;
                    case Operators.LessOrEqual:
                        result += " <= ";
                        break;
                    case Operators.LessThan:
                        result += " < ";
                        break;
                    case Operators.Like:
                        result += " LIKE ";
                        break;
                }

                result += GetParameterName();
            }

            if (compound)
            {
                foreach (Condition andCond in AndConditions)
                {
                    result += "AND " + andCond.ToString();
                }

                foreach (Condition orCond in OrConditions)
                {
                    result += "OR " + orCond.ToString();
                }

                result += ")";
            }

            return result;
        }
    }
}
