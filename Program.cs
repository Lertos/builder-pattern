using System.ComponentModel;
using System.Text;

namespace builder_pattern
{
    //A demonstration of the Builder pattern in C#
    public class Program
    {
        static void Main(string[] args)
        {
            //Create the builder and assign the builder to the director
            BurgerBuilder builder = new BurgerBuilder();
            Director director = new Director(builder);

            //Choose the desired burger and let the builder build it
            director.BuildVeggieBurger();
            Console.WriteLine(builder.GetBurger().ListParts());
            
            director.BuildCheeseBurger();
            Console.WriteLine(builder.GetBurger().ListParts());

            //You can also do this without the director

            builder.AddLettuce().AddHamburger().AddCheese().AddTomato();
            Console.WriteLine(builder.GetBurger().ListParts());

            /* OUTPUT
             * 
             * Your burger consists of: cheese, lettuce, tomato
             * Your burger consists of: cheese, hamburger
             * Your burger consists of: lettuce, hamburger, cheese, tomato
             */
        }
    }

    //Have the interface so we can have different builders if different functionality is required
    public interface IBurgerBuilder
    {
        //Return the builder object so we can chain method calls
        public IBurgerBuilder AddCheese();
        public IBurgerBuilder AddHamburger();
        public IBurgerBuilder AddTomato();
        public IBurgerBuilder AddLettuce();
    }

    //Implement the builder, providing concrete logic for the actual building of the burger
    public class BurgerBuilder : IBurgerBuilder
    {
        private Burger burger = new Burger();

        public BurgerBuilder()
        {
            this.ResetBuilder();
        }

        //Reset the builder so it can be re-used after a burger has been finished
        private void ResetBuilder()
        {
            burger = new Burger();
        }

        public IBurgerBuilder AddCheese()
        {
            burger.Add("cheese");
            return this;
        }

        public IBurgerBuilder AddHamburger()
        {
            burger.Add("hamburger");
            return this;
        }

        public IBurgerBuilder AddTomato()
        {
            burger.Add("tomato");
            return this;
        }

        public IBurgerBuilder AddLettuce()
        {
            burger.Add("lettuce");
            return this;
        }

        public Burger GetBurger()
        {
            Burger finishedBurger = burger;

            //Resetting as we should be able to reuse the builder for the next burger
            ResetBuilder();

            return finishedBurger;
        }
    }

    //Implement the burger as a list of parts
    public class Burger
    {
        public List<string> parts = new List<string>();

        public void Add(string part)
        {
            parts.Add(part);
        }

        public string ListParts()
        {
            StringBuilder sb = new StringBuilder();

            sb.Append("Your burger consists of: ");

            foreach (string part in parts)
            {
                sb.Append(part);
                sb.Append(", ");
            }

            sb.Remove(sb.Length - 2, 2);

            return sb.ToString();
        }
    }

    // NOTE: This object is completely optional. It only servers to abstracts the types of burgers
    //The director object is so we can provide specific options that are abstracted away
    public class Director
    {
        private IBurgerBuilder burgerBuilder;

        public Director(IBurgerBuilder burgerBuilder)
        {
            this.burgerBuilder = burgerBuilder;
        }

        public void BuildCheeseBurger()
        {
            burgerBuilder.AddCheese().AddHamburger();
        }

        public void BuildVeggieBurger()
        {
            burgerBuilder.AddCheese().AddLettuce().AddTomato();
        }
    }
}
