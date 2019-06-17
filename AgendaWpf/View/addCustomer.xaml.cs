using System;
using System.Collections.Generic;
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
    /// Logique d'interaction pour addCustomer.xaml
    /// </summary>
    public partial class addCustomer : Page
    {
        Model1 db = new Model1();

        public addCustomer()
        {
            InitializeComponent();
        }
        readonly string regexName = @"^[A-Za-zéàèêëïîç\- ]+";            //regex du nom, mail et telephone
        readonly string regexMail = @"^[a-z\-\.]+@[a-z\-\.]+[a-z]{2,4}";
        readonly string regexPhone = @"^0[0-9]{9}";

        /// <summary>
        /// Permet de vérifier le nom
        /// On verifie si il n'est pas null, si il passe la regex
        /// </summary>
        private bool Verif_Lastname()     // declaration d'un evenement bool et verifie si les champs sont valides
        {
            bool verifIsOK = false;
            if (!String.IsNullOrEmpty(TextBox_Lastname.Text))
            {
                if (!Regex.IsMatch(TextBox_Lastname.Text, regexName))
                {
                    TextBlock_LastnameErrorMessage.Text = "Saisie non valide";
                }
                else
                {
                    TextBlock_LastnameErrorMessage.Text = "";
                    verifIsOK = true;
                }
            }
            else
            {
                TextBlock_LastnameErrorMessage.Text = "Champs invalides";
            }
            return verifIsOK;
        }

        /// <summary>
        /// Permet de vérifier le prénom
        /// On vérifie si il n'est pas null, si il passe la regex
        /// </summary>
        private bool Verif_Firstname()
        {
            bool verifIsOK = false;
            if (!String.IsNullOrEmpty(TextBox_Firstname.Text))
            {
                if (!Regex.IsMatch(TextBox_Firstname.Text, regexName))
                {
                    TextBlock_FirstnameErrorMessage.Text = "Saisie non valide";
                }
                else
                {
                    TextBlock_FirstnameErrorMessage.Text = "";
                    verifIsOK = true;
                }
            }
            else
            {
                TextBlock_FirstnameErrorMessage.Text = "Champs invalides";
            }
            return verifIsOK;
        }

        /// <summary>
        /// Permet de vérifier le mail
        /// On vérifie si il n'est pas null, si il passe la regex, si le mail saisie est disponible
        /// </summary>
        private bool Verif_Mail()
        {
            bool verifIsOK = false; ;
            if (!String.IsNullOrEmpty(TextBox_Mail.Text))
            {
                if (!Regex.IsMatch(TextBox_Mail.Text, regexMail))
                {
                    TextBlock_MailErrorMessage.Text = "Saisie non valide";
                }
                else
                {
                    var mailDisponibility = db.customers.Where(x => x.mail == TextBox_Mail.Text).FirstOrDefault();
                    if (mailDisponibility != null)
                    {
                        TextBlock_MailErrorMessage.Text = "Mail incorrect";
                    }
                    else
                    {
                        TextBlock_MailErrorMessage.Text = "";
                        verifIsOK = true;
                    }
                }
            }
            else
            {
                TextBlock_MailErrorMessage.Text = "Champs invalides";
            }
            return verifIsOK;
        }

        /// <summary>
        /// Permet de vérifier le numéro de téléphone
        /// On vérifie si il n'est pas null, si il passe la regex
        /// </summary>
        private bool Verif_PhoneNumber()
        {
            bool verifIsOK = false;
            if (!String.IsNullOrEmpty(TextBox_PhoneNumber.Text))
            {
                if (!Regex.IsMatch(TextBox_PhoneNumber.Text, regexPhone))
                {
                    TextBlock_PhoneNumberErrorMessage.Text = "Saisie non valide";
                }
                else
                {
                    TextBlock_PhoneNumberErrorMessage.Text = "";
                    verifIsOK = true;
                }
            }
            else
            {
                TextBlock_PhoneNumberErrorMessage.Text = "Champs invalides";
            }
            return verifIsOK;
        }

        /// <summary>
        /// Permet de vérifier le budget
        /// On vérifie si il n'est pas null ou si la saisie peux être parse en int et quelle est supérieure a 0
        /// </summary>
        private bool Verif_Budget()
        {
            bool verifIsOK = false;
            if (!String.IsNullOrEmpty(TextBox_Budget.Text))
            {
                bool budgetIsNum = int.TryParse(TextBox_Budget.Text, out int budget);
                if (budgetIsNum == false || budget < 0)
                {
                    TextBlock_BudgetErrorMessage.Text = "Saisie non valide";
                }
                else
                {
                    TextBlock_BudgetErrorMessage.Text = "";
                    verifIsOK = true;
                }
            }
            else
            {
                {
                    TextBlock_BudgetErrorMessage.Text = "Champs invalides";
                }
            }
            return verifIsOK;
        }

        /// <summary>
        /// Méthode évenement KeyUp du TextBox du nom
        /// Lance la méthode de vérif du nom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Lastname_KeyUp(object sender, KeyEventArgs e)
        {
            Verif_Lastname();
        }

        /// <summary>
        /// Méthode évenement KeyUp du TextBox du prénom
        /// Lance la méthode de vérif du prénom
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Firstname_KeyUp(object sender, KeyEventArgs e)
        {
            Verif_Firstname();
        }

        /// <summary>
        /// Méthode évenement KeyUp du TextBox du mail
        /// Lance la méthode de vérif du mail
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Mail_KeyUp(object sender, KeyEventArgs e)
        {
            Verif_Mail();
        }

        /// <summary>
        /// Méthode évenement KeyUp du TextBox du numéro de téléphone
        /// Lance la méthode de vérif du numéro de téléphone
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_PhoneNumber_KeyUp(object sender, KeyEventArgs e)
        {
            Verif_PhoneNumber();
        }

        /// <summary>
        /// Méthode évenement KeyUp du TextBox du budget
        /// Lance la méthode de vérif du budget
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void TextBox_Budget_KeyUp(object sender, KeyEventArgs e)
        {
            Verif_Budget();
        }

        /// <summary>
        /// Permet d'ajouter un clients a la DB
        /// On appelle tout nos méthode de vérif si tout est ok on ajoute
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_AddCustomer_Click(object sender, RoutedEventArgs e)
        {
            if (Verif_Lastname() && Verif_Firstname() && Verif_Mail() && Verif_PhoneNumber() && Verif_Budget())
            {
                customers addCustomer = new customers()
                {
                    lastname = TextBox_Lastname.Text,
                    firstname = TextBox_Firstname.Text,
                    mail = TextBox_Mail.Text,
                    phonenumber = TextBox_PhoneNumber.Text,
                    budget = int.Parse(TextBox_Budget.Text)
                };
                db.customers.Add(addCustomer);
                db.SaveChanges();
                TextBlock_SuccesMessage.Text = "Ajout d'un client réussie";
            }
        }

        /// <summary>
        /// Permet d'annuler l'ajout
        /// Vide tout les text_box
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Button_CancelAddCustomer_Click(object sender, RoutedEventArgs e)
        {
            this.NavigationService.Navigate(new System.Uri("View/customersList.xaml", UriKind.RelativeOrAbsolute)); // lien de navigation, renvoi à la page customerList si on appuis sur le bouton annuler
            TextBox_Firstname.Text = "";
            TextBox_Lastname.Text = "";
            TextBox_Mail.Text = "";
            TextBox_PhoneNumber.Text = "";
            TextBox_Budget.Text = "";
        }
    }
}







