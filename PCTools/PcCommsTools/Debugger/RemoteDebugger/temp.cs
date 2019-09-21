using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace RemoteDebugger
{
	class temp
	{

		public void TestInterfaceWeapon()
		{

			InterfaceWeaponA iwa = new InterfaceWeaponA();
			iwa.Fire();
			iwa.Name();

			InterfaceWeaponB iwb = new InterfaceWeaponB();
			iwb.Fire();
			iwb.Name();



		}


	}






	//We define a interface to make sure both Name and Fire functions are supplied
	interface IWeapon
	{ 
		bool Fire();
		string Name();

	}
	
	//This class must supply the two interface functions Fire & Name, if they are now supplied we will get a error report and compile time.
	public class InterfaceWeaponA : IWeapon
	{

		public string Name()
		{
			return ("InterfaceWeaponA");
		}

		public virtual bool Fire()
		{
			Console.WriteLine("InterfaceWeaponA Fire Weapon");
			return true;
		}
	}



	//This class derives from InterfaceWeaponA but does nto need to supply Name function because InterfaceWeaponA already supplies that.
	// we do override Fire function on this class though
	public class InterfaceWeaponB : InterfaceWeaponA
	{

		public override bool Fire()
		{
			Console.WriteLine("InterfaceWeaponB Fire Weapon");
			return true;
		}
	}




	//This version of the class is not using the interface (its rare that you would use both) but is abstract
	public abstract class AbstractWeaponA
	{

		public string Name()
		{
			return ("AbstractWeaponA");
		}

		public virtual bool Fire()
		{
			Console.WriteLine("AbstractWeaponA Fire Weapon");
			return true;
		}
	}



	public class AbstractWeaponB : AbstractWeaponA
	{

		public string Name()
		{
			return ("AbstractWeaponB");
		}

		public override bool Fire()
		{
			Console.WriteLine("AbstractWeaponB Fire Weapon");
			return true;
		}
	}



}
