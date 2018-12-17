namespace RuleEngineTester.Entities
{
    public class Car : BaseVehicle, ICar
    {
        public Car() { }
        public Car(int year, string make, string style)
        {
            Year = year;
            Make = make;
            Style = style;
        }
    }
}
