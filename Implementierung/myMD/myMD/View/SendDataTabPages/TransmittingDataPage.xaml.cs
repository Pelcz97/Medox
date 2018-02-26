﻿using System;
using System.Collections.Generic;
using myMD.View.AbstractPages;
using myMD.ViewModel.SendDataTabViewModel;
using Xamarin.Forms;
using Xamarin.Forms.Internals;

namespace myMD.View.SendDataTabPages
{
    /// <summary>
    /// Code-Behind Klasse zurTransmittingDataPage.
    /// </summary>
    [Preserve(AllMembers = true)]
    public partial class TransmittingDataPage : CustomContentPage
    {
        /// <summary>
        /// Das ViewModel, zu welchem gebinded werden soll.
        /// </summary>
        TransmittingDataViewModel vm;

        /// <summary>
        /// Initializes a new instance of the <see cref="T:myMD.View.SendDataTabPages.TransmittingDataPage"/> class.
        /// </summary>
        public TransmittingDataPage()
        {
            InitializeComponent();
            vm = new TransmittingDataViewModel();
            BindingContext = vm;
        }

       
    }
}
