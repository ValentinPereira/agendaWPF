using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace AgendaWpf.View
{
    /// <summary>
    /// Logique d'interaction pour addAppointment.xaml
    /// </summary>
    public partial class addAppointment : Page
    {
        Model1 db = new Model1();
        public addAppointment()
        {
            InitializeComponent();
            var customersList = db.customers.ToList();
            customersComboBox.DataContext = customersList;
            var brokersList = db.brokers.ToList();
            brokersComboBox.DataContext = brokersList;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            // Déclaration d'une variable de type boolean isValid avec une valeur vrai
            bool isValid = true;
            /** Vérification si la valeur de brokersComboBox est vide
             * Si elle est vide, modification de isValid en false empêchant l'ajout du rendez-vous
             * Affichage du message d'erreur approprié
            **/
            if (brokersComboBox.SelectedValue.ToString() == null)
            {
                isValid = false;
                MessageBox.Show("Veuillez sélectionner un courtier", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            /** Vérification si la valeur de customersComboBox est vide
            * Si elle est vide, modification de isValid en false empêchant l'ajout du rendez-vous
            * Affichage du message d'erreur approprié
            **/
            if (customersComboBox.SelectedValue.ToString() == null)
            {
                isValid = false;
                MessageBox.Show("Veuillez sélectionner un client", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            /** Vérification si le textbox sujet est vide
            * Si elle est vide, modification de isValid en false empêchant l'ajout du rendez-vous
            * Affichage du message d'erreur approprié
            **/
            if (SubjectTextBox.Text == null)
            {
                isValid = false;
                MessageBox.Show("Veuillez entrer un sujet", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            /** Vérification si le contenue de textboxhour est vide
            * Si elle est vide, modification de isValid en false empêchant l'ajout du rendez-vous
            * Affichage du message d'erreur approprié
            **/
            if (HoursTextBox.Text == null)
            {
                isValid = false;
                MessageBox.Show("Veuillez entrer une heure", "Erreur");
            }
            /**
             * Si textboxhour n'est pas vide
             * Déclaration d'une variable de type boolean hourIsNum et tentative de parse de textboxhour en int
             **/
            else
            {
                bool hourIsNum = int.TryParse(HoursTextBox.Text, out int hourNum);
                /**
                 *  Si hourIsNum est faux
                 *  Modification de la valeur de isValid
                 *  Affichage d'un message d'erreur
                 **/
                if (!hourIsNum)
                {
                    isValid = false;
                    MessageBox.Show("L'heure indiqué n'est pas valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                /**
                 * Si hourIsNum est vrai
                 **/
                else
                {
                    /**
                     * Si hourNum est inférieur à 8 ou supérieure à 18
                     **/
                    if (hourNum < 8 || hourNum > 18)
                    {
                        /**
                         * Modification de isValid en faux
                         * Affichage du message d'erreur
                         **/
                        isValid = false;
                        MessageBox.Show("Veuillez entrer une heure d'ouverte", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            /**
             * Si TextBoxMinute est vide
             **/
            if (MinutesTextBox.Text == null)
            {
                /**
                 * Modification de isValid en faux
                 * Affichage du message d'erreur
                 **/
                isValid = false;
                MessageBox.Show("Veuillez entrer des minutes", "Erreur");
            }
            else
            {
                /**
                 * Déclaration du boolean minuteIsNum et tentative de parse en int le contenue de textboxhour, déclaration de minuteNum avec le contenue parse 
                 **/
                bool minuteIsNum = int.TryParse(HoursTextBox.Text, out int minuteNum);
                /**
                 * Si minuteIsNum est faux
                 **/
                if (!minuteIsNum)
                {
                    /**
                     * isValid deviens faux
                     * Affichage du message d'erreur
                     **/
                    isValid = false;
                    MessageBox.Show("Veuillez entrer des chiffres pour vos minutes", "Erreur");
                }
                else
                {
                    /**
                     * Si minuteNum est inférieure à 0 ou supérieur à 59
                     **/
                    if (minuteNum < 0 || minuteNum > 59)
                    {
                        /**
                         * isValid deviens faux
                         * Affichage du message d'erreur
                         **/
                        isValid = false;
                        MessageBox.Show("Veuillez entrer une heure valide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                }
            }
            /**
             * Vérification du contenue de datepickerday
             **/
            if (String.IsNullOrEmpty(DatePickerDay.Text))
            {
                /**
                 * isValid deviens faux
                 * Affichage du message d'erreur
                 **/
                isValid = false;
                MessageBox.Show("Vous ne pouvez pas mettre une date vide", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            else
            {
                /**
                 * Convertis datepickerday.text en datetime, puis vérification si la date est inférieure à la date actuelle
                 **/
                if (Convert.ToDateTime(DatePickerDay.Text) < DateTime.Now)
                {
                    /**
                     * isValid deviens faux
                     * Affichage du message d'erreur
                     **/
                    isValid = false;
                    MessageBox.Show("Veuillez entrer une date supérieure à la date actuelle", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                /**
                 * Convertis datepickerday.text en datetime puis récupération des jours de la semaine, comparaison pour savoir si c'est égale à samedi ou dimanche
                 **/
                if (Convert.ToDateTime(DatePickerDay.Text).DayOfWeek.ToString() == "Saturday" || Convert.ToDateTime(DatePickerDay.Text).DayOfWeek.ToString() == "Sunday")
                {
                    /**
                     * isValid devient faux
                     * Affichage du message d'erreur
                     **/
                    isValid = false;
                    MessageBox.Show("Impossible de prendre rendez-vous le week-end", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
            /**
             * Si isValid est vrai
             **/
            if (isValid)
            {
                try
                {
                    /**
                     * Déclaration d'une variable date et concaténation entre datepickerday, textboxhour et textboxminute
                     * Déclaration d'un datetime selecteddate et attribution de la valeur en convertissant date
                     **/
                    var date = DatePickerDay.Text + " " + HoursTextBox.Text + ":" + MinutesTextBox.Text;
                    DateTime SelectedDate = Convert.ToDateTime(date);
                    /**
                     * Déclaration de l'objet addAppointment et instancie la classe appointments
                     **/
                    appointments AddAppointment = new appointments()
                    {
                        // attribution des valeurs à mes attributs de l'objet addAppointment
                        idBrokers = Convert.ToInt32(brokersComboBox.SelectedValue),
                        idCustomers = Convert.ToInt32(customersComboBox.SelectedValue),
                        subject = SubjectTextBox.Text,
                        datehour = SelectedDate
                    };
                    /**
                     * Enregistrement de mon objet addAppointment dans ma base de données db dans la table appointment
                     **/
                    db.appointments.Add(AddAppointment);
                    db.SaveChanges();
                    MessageBox.Show("Le rendez-vous a été ajouté avec succès!", "Confirmation", MessageBoxButton.OK, MessageBoxImage.Information);
                }
                catch
                {
                    MessageBox.Show("Impossible d'enregistrer votre rendez-vous", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                }
            }
        }
        private void PageList_Loaded(object sender, RoutedEventArgs e)
        {
            db.brokers.Load();
            db.customers.Load();
        }
    }
}
