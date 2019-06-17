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
    /// Logique d'interaction pour brokersList.xaml
    /// </summary>
    public partial class brokersList : Page
    {
        Model1 db = new Model1();
        private CollectionViewSource custViewSource;

        public brokersList()
        {
            InitializeComponent();
            custViewSource = (CollectionViewSource)FindResource("brokersViewSource");
        }
        private void PageListBroker_Loaded(object sender, RoutedEventArgs e)
        {
            // chargement data table customers
            db.brokers.Load();

            // sctockage de la data dans la source de la collection
            custViewSource.Source = db.brokers.Local;
        }
        private void Update_Click(object sender, RoutedEventArgs e)
        {
            string mailRegex = @"^[\w\.=-]+@[\w\.-]+\.[\w]{2,3}$";
            string phoneNumberRegex = @"^0[0-9]{9}$";
            string LastnameRegex = @"^[A-Za-zéàèêëïîç\- ]+$";
            bool isValid = true;
            int idBroker = int.Parse(TextBoxIdBroker.Text);
            TextBlockErrorPhoneNumber.Text = "";
            TextBlockErrorLastname.Text = "";
            TextBlockErrorFirstname.Text = "";
            TextBlockErrorMail.Text = "";
            // si les champs sont corrects continuer la vérification, sinon  faire apparaitre un message d'erreur
            if (!string.IsNullOrEmpty(TextBoxLastName.Text) && !string.IsNullOrEmpty(TextBoxFirstName.Text) && !string.IsNullOrEmpty(TextBoxEmail.Text) && !string.IsNullOrEmpty(TextBoxPhoneNumber.Text))
            {
                // vérifie la Regex avec le champs textboslastname et la variable apeller lastnameregex
                if (!Regex.IsMatch(TextBoxLastName.Text, LastnameRegex))
                {
                    TextBlockErrorLastname.Text = "Erreur le nom est incorrect";
                    isValid = false; // Confirme que la condition est fausse par la propriété bool et affiche un message d'erreur
                }
                if (!Regex.IsMatch(TextBoxFirstName.Text, LastnameRegex))
                {
                    TextBlockErrorFirstname.Text = "Erreur le prénom est incorrect";
                    isValid = false;
                }
                if (!Regex.IsMatch(TextBoxEmail.Text, mailRegex))
                {
                    TextBlockErrorMail.Text = "Erreur le mail est incorrect";
                    isValid = false;
                }
                else if (db.brokers.Any(x => x.mail == TextBoxEmail.Text & x.idBrokers!= idBroker))
                {
                    TextBlockErrorMail.Text = "Cette adresse mail existe déjà";
                    isValid = false;
                }


                if (!Regex.IsMatch(TextBoxPhoneNumber.Text, phoneNumberRegex))
                {
                    TextBlockErrorPhoneNumber.Text = "Erreur le numéro est incorrect";
                    isValid = false;
                }
                if (isValid)
                {
                    try
                    {
                        brokers brokerToUpdate = new brokers() // declaration de l'objet customerToUpdate et l'instancie avec la class customers
                        {
                            lastname = TextBoxLastName.Text, // J'attribue la valeur à l'attribut lastname de l'objet customerToUpdate
                            firstname = TextBoxFirstName.Text,
                            mail = TextBoxEmail.Text,
                            phonenumber = TextBoxPhoneNumber.Text,
                        };
                        db.SaveChanges(); //Enregistre 

                        //Message de mofifications et d'erreurs
                        MessageBox.Show("Le client a bien été modifié.", "Client modifié", MessageBoxButton.OK);
                    }
                    catch
                    {
                        MessageBox.Show("Une erreur s'est produite, veuillez réessayer.", "Erreur", MessageBoxButton.OK, MessageBoxImage.Error); // Si il y a une erreur pendant l'enregistrement affiche un message d'erreur
                    }
                }
            }
        }
        private void ButtonCancel_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult answer = MessageBox.Show("Etes-vous sûr de vouloir supprimer ? ", "Attention", MessageBoxButton.YesNo, MessageBoxImage.Warning); //Ouvre une messagebox qui demande si l'on veut vraiment supprimer le client
            if (answer == MessageBoxResult.Yes) // Condition qui vérifie si l'objet answer est égal à la boxresult, si l'utilisateur dit oui continuer la procédure de suppression
            {
                try
                {
                    db.brokers.Remove(db.brokers.Find(int.Parse(TextBoxIdBroker.Text))); //Supression de l'élément demandé, conversion avec int parse de l'id Textblock
                    db.SaveChanges(); // Enregistre la modification
                    MessageBox.Show("Courtier supprimé avec succès", "Suppression réussie", MessageBoxButton.OK); //Affiche que le client est supprimé avec succés
                }
                catch
                {
                    MessageBox.Show("Une erreur s'est produite, veuillez réessayer ultérieurement", "Erreur", MessageBoxButton.OK, MessageBoxImage.Information); // Si une erreur ce produit pendant la suppression affiche un message d'erreur
                }
            }
        }
    }
}
