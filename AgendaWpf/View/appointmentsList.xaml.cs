using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Text;
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
    /// // int, var ... = type
    /// // class
    /// // attribut
    /// // objet
    /// // opérateur de comparaison
    /// // événement
    /// Logique d'interaction pour appointmentsList.xaml
    /// </summary>
    public partial class appointmentsList : Page
    {
        Model1 db = new Model1();
        private CollectionViewSource appointmentsViewSource;

        public appointmentsList()
        {
            InitializeComponent();
            var customersList = db.customers.ToList();
            customersComboBox.DataContext = customersList;
            var brokersList = db.brokers.ToList();
            brokersComboBox.DataContext = brokersList;

            appointmentsViewSource = (CollectionViewSource)FindResource("appointmentsViewSource"); // récupére la source dans le xaml
        }
        private void PageListAppointment_Loaded(object sender, RoutedEventArgs e)
        {
            // chargement data table customers
            db.appointments.Load();

            // stockage de la data dans la source de la collection
            appointmentsViewSource.Source = db.appointments.Local;
        }

        private void ButtonSave_Click(object sender, RoutedEventArgs e)
        {
            bool isValid = true;

            if (brokersComboBox.SelectedValue.ToString() == null) // si rien n'est selectionné dans la combobox affiche un message d'erreur
            {
                MessageBox.Show("Veuillez entrer un nom", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }
            if (customersComboBox.SelectedValue.ToString() == null)
            {
                MessageBox.Show("Veuillez entrer un nom", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
                isValid = false;
            }
            if (TextBoxSubject.Text == null)
            {
                isValid = false;
                MessageBox.Show("Veuillez entrer un sujet", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error);
            }
            if (TextBoxHour.Text == null)
            {
                isValid = false;
                MessageBox.Show("Veuillez sélectionner une heure", "Erreur");
            }
            else
            {
                bool hourBox = int.TryParse(TextBoxHour.Text, out int hourBoxNum);

                if (!hourBox) // Si l'heure choisis n'est pas un chiffre on indique une erreur 
                {
                    isValid = false;
                    MessageBox.Show("L'heure indiqué n'est pas valide", "Erreur heure invalide", MessageBoxButton.OK, MessageBoxImage.Error);
                }
                else
                {
                    /**
                     * Si hourNum est inférieur à 8 ou supérieure à 18
                     **/
                    if (hourBoxNum < 8 || hourBoxNum > 18)
                    {
                        /**
                         * Modification de isValid en faux
                         * Affichage du message d'erreur
                         **/
                        isValid = false;
                        MessageBox.Show("Veuillez entrer une heure valide de rendez-vous", "Erreur heure", MessageBoxButton.OK, MessageBoxImage.Error);
                    }
                    if (TextBoxMinute.Text == null)
                    {
                        /**
                         * Modification de isValid en faux
                         * Affichage du message d'erreur
                         **/
                        isValid = false;
                        MessageBox.Show("Veuillez entrer des minutes", "Erreur minute invalide");
                    }
                    else
                    {
                        /**
                         * Déclaration du boolean minuteIsNum et tentative de parse en int le contenue de textboxhour, déclaration de minuteNum avec le contenue parse 
                         **/
                        bool minuteIsNum = int.TryParse(TextBoxHour.Text, out int minuteNum);
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
                            MessageBox.Show("Veuillez entrer des chiffres pour vos minutes", "Erreur minute invalide");
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
                                MessageBox.Show("Veuillez entrer une heure valide", "Erreur heure invalide", MessageBoxButton.OK, MessageBoxImage.Error);
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
                        MessageBox.Show("Vous ne pouvez pas mettre une date vide", "Erreur de date", MessageBoxButton.OK, MessageBoxImage.Error);
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
                            MessageBox.Show("Veuillez entrer une date supérieure à la date actuelle", "Erreur rendez-vous", MessageBoxButton.OK, MessageBoxImage.Error);
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
                            MessageBox.Show("Impossible de selectionner un rendez-vous le week-end", "Erreur rendez-vous", MessageBoxButton.OK, MessageBoxImage.Error);
                        }
                        if (isValid)
                        {
                            try
                            {
                                var date = DatePickerDay.Text + " " + TextBoxHour.Text + ":" + TextBoxMinute.Text;
                                DateTime SelectedDate = Convert.ToDateTime(date);

                                appointments appointmentModify = new appointments()
                                {
                                    idBrokers = Convert.ToInt32(brokersComboBox.SelectedValue),
                                    idCustomers = Convert.ToInt32(customersComboBox.SelectedValue),
                                    datehour = Convert.ToDateTime(date),
                                    subject = TextBoxSubject.Text
                                };

                                
                                db.SaveChanges();

                                MessageBox.Show("Le courtier à bien été modifié.", "Modification", MessageBoxButton.OK);
                            }
                            catch
                            {
                                MessageBox.Show("Une erreure s'est produite, veuillez réessayer plus tard.", "Modification erreur", MessageBoxButton.OK);
                            }

                            //private void ButtonDelete_Click(object sender, RoutedEventArgs e)
                            //{
                            //    {
                            //        MessageBoxResult choose = MessageBox.Show("Etes-vous sûr de vouloir supprimer ce rendez-vous?", "Confirmation", MessageBoxButton.YesNo, MessageBoxImage.Exclamation); // affiche un message de confirmation avec la methode resultat
                            //        if (choose == MessageBoxResult.Yes)
                            //        {
                            //            try
                            //            {
                            //                db.appointments.Remove(db.appointments.Find(int.Parse(idAppointmentTextBlock.Text))); // je supprime la méthode remove pour supprimer l'element dans la table appointment à supprimer
                            //                db.SaveChanges(); //enregistre la modification 
                            //                                  //Affiche un message d'erreur ou de succés avec la methode messagebox
                            //                MessageBox.Show("Rendez-vous supprimé avec succès", "Suppression réussie", MessageBoxButton.OK);
                            //            }
                            //            catch
                            //            {
                            //                MessageBox.Show("Une erreur s'est produite", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information);
                            //            }
                            //        }
                                }
                            }
                        }
                    }
                }
            }
        }
