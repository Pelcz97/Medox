using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NUnit.Framework;
using myMD.ViewModel.OverviewTabViewModel;
using myMD.ModelInterface.DataModelInterface;
using myMDTests.Model.EntityFactory;
using Xamarin.Forms;
using myMDTests.ViewModel;
using myMD;

namespace myMDTests.ViewModel.OverviewTab
{
    class OverviewPageTests
    {
        /*
        RandomEntityFactory factory = new RandomEntityFactory();
        App app;
        OverviewViewModel vm;
        DoctorsLetterViewModel DoctorsLetterVM1;
        DoctorsLetterViewModel DoctorsLetterVM2;

        [SetUp]
        public void Setup()
        {
            app = new myMD.App;
            vm = new OverviewViewModel();
            DoctorsLetterVM1 = new DoctorsLetterViewModel(factory.ILetter());
            DoctorsLetterVM2 = new DoctorsLetterViewModel(factory.ILetter());
        }
        
        [Test]
        public void GroupListTest()
        {

            vm.DoctorsLettersList.Add(DoctorsLetterVM1);
            vm.DoctorsLettersList.Add(DoctorsLetterVM2);
            DoctorsLetterViewModel First = vm.DoctorsLettersList.ElementAt(0);
            DoctorsLetterViewModel Second = vm.DoctorsLettersList.ElementAt(1);
            Assert.IsTrue(First.DoctorsLetterDate.Date.CompareTo(Second.DoctorsLetterDate.Date) <= 0);
        }
        */
    }
}
