using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using PayMint.StaticsAndConstants;
using PayMint.Models;
using PayMint.Controls;
using System.Configuration;

namespace PayMint
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        #region Variables
        IWorkDetailsModel wdmWorkDetails;
        IBonusFeeModel bfmPayment;
        ITableBuilder PaymentTable;
        #endregion

        public MainWindow()
        {
            InitializeComponent();
            //Initialize tbFee textbox
            tbFee.Text = AppMessages.feePlaceHolder;
            tbFee.GotFocus += delegate (object sender, RoutedEventArgs e) { RemoveText(sender, e, AppMessages.feePlaceHolder); };
            tbFee.LostFocus += delegate (object sender, RoutedEventArgs e) { AddText(sender, e, AppMessages.feePlaceHolder); };

            //Initialize tbDailyHoursWorked textbox
            tbDailyHoursWorked.Text = AppMessages.dailyHoursPlaceHolder;
            tbDailyHoursWorked.GotFocus += delegate (object sender, RoutedEventArgs e) { RemoveText(sender, e, AppMessages.dailyHoursPlaceHolder); };
            tbDailyHoursWorked.LostFocus += delegate (object sender, RoutedEventArgs e) { AddText(sender, e, AppMessages.dailyHoursPlaceHolder); };

            //Initialize tbBonusPercent textbox
            tbBonusPercent.Text = AppMessages.bonusPlaceHolder;
            tbBonusPercent.GotFocus += delegate (object sender, RoutedEventArgs e) { RemoveText(sender, e, AppMessages.bonusPlaceHolder); };
            tbBonusPercent.LostFocus += delegate (object sender, RoutedEventArgs e) { AddText(sender, e, AppMessages.bonusPlaceHolder); };

            //Initialize cbBlackOutWeekEnds checked event
            cbBlackOutWeekEnds.Checked += delegate (object sender, RoutedEventArgs e) { BlackOutSatsAndSunds(sender, e, calWorkDays); };
            BlackOutSatsAndSunds(cbBlackOutWeekEnds, new RoutedEventArgs(), calWorkDays);

            //Loading Configuration
            if (ConfigurationManager.AppSettings.AllKeys.Contains("Fee"))
            {
                tbFee.Text = ConfigurationManager.AppSettings.Get("Fee");
            }
            
            if (ConfigurationManager.AppSettings.AllKeys.Contains("WorkHours"))
            {
                tbDailyHoursWorked.Text = ConfigurationManager.AppSettings.Get("WorkHours");
            }
            
            if (ConfigurationManager.AppSettings.AllKeys.Contains("BonusFee"))
            {
                tbBonusPercent.Text = ConfigurationManager.AppSettings.Get("BonusFee");
            }

            //Only the important cells should be copied
            dgPayMintAble.ClipboardCopyMode = DataGridClipboardCopyMode.ExcludeHeader;
        }

        #region EventHandlers

        #region VSManagedEvents
        /// <summary>
        /// Occurs when the user clicks on top of the calculate request table button.
        /// </summary>
        /// <param name="sender">Button clicked</param>
        /// <param name="e">Button parameters</param>
        private void btCalculate_Click(object sender, RoutedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tbFee.Text) || tbFee.Text.Equals(AppMessages.feePlaceHolder))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(tbDailyHoursWorked.Text) || tbDailyHoursWorked.Text.Equals(AppMessages.dailyHoursPlaceHolder))
            {
                return;
            }

            if (string.IsNullOrWhiteSpace(tbBonusPercent.Text) || tbBonusPercent.Text.Equals(AppMessages.bonusPlaceHolder))
            {
                return;
            }

            wdmWorkDetails = new WorkDetailsModel();
            wdmWorkDetails.DailyWorkedHours = Convert.ToDecimal(tbDailyHoursWorked.Text);
            wdmWorkDetails.DailyWorkHours = Convert.ToDecimal(AppParameters.DailyWorkHours);
            wdmWorkDetails.WorkMonth = calWorkDays.SelectedDates.Count > 0 ? calWorkDays.SelectedDates[0] : DateTime.Today;
            wdmWorkDetails.WorkedDays = calWorkDays.SelectedDates.Count;

            bfmPayment = new BonusFeeModel();
            bfmPayment.workDetails = wdmWorkDetails;
            if (ConfigurationManager.AppSettings.AllKeys.Contains("Currency"))
            {
                bfmPayment.Currency = ConfigurationManager.AppSettings.Get("Currency");
            }
            bfmPayment.BonusPercent = Convert.ToDecimal(tbBonusPercent.Text);
            bfmPayment.MonthlyFee = Convert.ToDecimal(tbFee.Text);

            PaymentTable = new TableBuilder(bfmPayment);
            dgPayMintAble.ItemsSource = PaymentTable.GenerateTable();
            gFinalTable.Visibility = Visibility.Visible;
        }


        #endregion

        #region DevManagedEvents
        /// <summary>
        /// Event Method to remove placeholder text while waiting for user input
        /// </summary>
        /// <param name="sender">Textbox reference</param>
        /// <param name="e">Textbox Parameters</param>
        /// <param name="PlaceHolderTxt">Text to remove from the textbox</param>
        public void RemoveText(object sender, RoutedEventArgs e, string PlaceHolderTxt)
        {
            TextBox tbTemp = (TextBox)sender;
            if (tbTemp.Text == PlaceHolderTxt)
            {
                tbTemp.Text = string.Empty;
            }
        }

        /// <summary>
        /// Event Method to place text while waiting for user input into a textbox
        /// </summary>
        /// <param name="sender">Textbox reference</param>
        /// <param name="e">Textbox Parameters</param>
        /// <param name="PlaceHolderTxt">Text to place in the textbox</param>
        public void AddText(object sender, RoutedEventArgs e, string PlaceHolderTxt)
        {
            TextBox tbTemp = (TextBox)sender;
            if (string.IsNullOrWhiteSpace(tbTemp.Text))
                tbTemp.Text = PlaceHolderTxt;
        }

        /// <summary>
        /// Go through the dates and make sure weekends are blacked out.
        /// </summary>
        /// <param name="sender">Checkbox Control Object</param>
        /// <param name="e">Checkbox Event Arguments</param>
        /// <param name="calControl">Calendar Control to Modify</param>
        public void BlackOutSatsAndSunds(object sender, RoutedEventArgs e, Calendar calControl)
        {
            CheckBox cbTemp = (CheckBox)sender;
            DateTime dtStart;
            DateTime dtEnd;

            if (calControl.SelectedDates.Count > 0)
            {
                dtStart = calControl.SelectedDates[0];
            }else
            {
                dtStart = calControl.DisplayDate;
            }

            dtEnd = Convert.ToDateTime($"{DateTime.DaysInMonth(dtStart.Year, dtStart.Month)}-{dtStart.Month}-{dtStart.Year}");
            dtStart = Convert.ToDateTime($"01-{dtStart.Month}-{dtStart.Year}");

            if ((bool)cbTemp.IsChecked)
            {
                if (dtStart.DayOfWeek == DayOfWeek.Sunday)
                {
                    calControl.BlackoutDates.Add(new CalendarDateRange(dtStart));
                }

                foreach(DateTime dtDay in Utils.EachDayBetween(dtStart, dtEnd))
                {
                    if (dtDay.DayOfWeek == DayOfWeek.Saturday)
                    {
                        calControl.BlackoutDates.Add(new CalendarDateRange(dtDay, dtDay.AddDays(1)));
                    }
                }
            }
            else
            {
                calControl.BlackoutDates.Clear();
            }
        }
        #endregion

        #endregion
    }
}
