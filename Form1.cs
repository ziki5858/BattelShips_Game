using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace BattelShips
{
    public partial class Form1 : Form
    {
     
          public Form1()
        {
            InitializeComponent();
            radioButton1.Visible = false; radioButton2.Visible = false;
            button5.Visible = false; button3.Visible = false;
        }

        #region הגדרת משתנים
        Button[,] r;// מערך הכפתורים של הלוח
        int[,] n; // מערך האינטים
        Random ship = new Random();
        int tor = 0; // בשביל איזה ספינה מציבים עכשיו
        int verti=0,hori=-1;
        int X, Y, lastHitX = -1, lastHitY = -1;// הלחיצה האחרונה והמכה האחרונה
        int whonow = 0; // של מי הטור שתוקפים
        int player = 0;// אם זה שחקן א או ב
        int integer;
        int shipszie = 0; // גדול הספינה מטקסט בוקס 2
        int amsh = 0;// בשביל גודל המשחק- טקסט בוקס 1
        bool againstcom; 
        int sides = 0, amuda = 0;// בשביל עמודה או שורה
        int bomba = 0, bombb = 0;// יהיו שוות לגודל הספינות ודרכם מציגים ניצחון
        int newGame = 0; 
        #endregion

        #region כפתורי התחלת משחק

        #region  כשלוחצים על 1 נגד 1
        private void button1_Click(object sender, EventArgs e)
        {
            againstcom = false; button6.Visible = false;
            if (newGame == 0)
                Bniatloach();
            else
                Resetloach();
        }
        #endregion

        #region כשלוחצים על נגד מחשב
        private void button4_Click(object sender, EventArgs e)
        {           
            againstcom = true; button6.Visible = true;
            if (newGame == 0)
                Bniatloach();
            else
                Resetloach();
        }
        #endregion
        #endregion
   
        bool GetTextbox()
        {
            bool b = false;
            b = int.TryParse(textBox1.Text, out amsh);
            if (!b)
                 MessageBox.Show("You can only select 13,15,17");
            else
                b = true;
            // מתרגם מה שהוקלד לעמוד שורה
            if (amsh != 13 && amsh != 15 && amsh != 17)
            {
                MessageBox.Show("You can only select 13,15,17");
                return false;
            }

            bool a = false;
            a = int.TryParse(textBox2.Text, out shipszie);
            if (!a)
                MessageBox.Show("You can only select 4,5,6");
            else
                a = true;
            if (shipszie != 4 && shipszie != 5 && shipszie != 6)
            {
                MessageBox.Show("You can only select 4,5,6");
                return false;
            }
            return true;
        }
        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            if (int.Parse(textBox1.Text) == 13 || int.Parse(textBox1.Text) == 15 || int.Parse(textBox1.Text) == 17)
                Bniatloach();
        }
        void Resetloach()
        {
            if (GetTextbox())
            {
                label1.Visible = true; label2.Visible = true;
                textBox1.Visible = true; textBox2.Visible = true;
                button5.Visible = false; button3.Visible = false;
                radioButton1.Visible = true; radioButton2.Visible = true;
                tor = 0; amuda = 0; sides = 0; player = 0; whonow = 0;
                this.BackColor = Color.Cyan; newGame = 1; label7.Text = "";
                bomba = shipszie + shipszie - 1 + shipszie - 2 + shipszie - 3 + shipszie - 4;
                bombb = bomba;
                label3.Text = bomba.ToString(); label4.Text = bombb.ToString();
                for (int i = 0; i < r.GetLength(0); i++)
                {
                    for (int x = 0; x < r.GetLength(1); x++)
                    {
                        r[i, x].BackColor = Color.Blue;
                        n[i, x] = 0;
                        if (x != 0)
                        {
                            if (x == amsh / 2)
                            {
                                r[i, x].BackColor = Color.Green;
                                n[i, x] = 5;
                            }
                        }

                    }
                }
            }
        }
        void Bniatloach()
        {
           
            if (GetTextbox())
            {
                panel1.Controls.Clear();
                newGame++;
                int counter = 0;
                bomba = shipszie + shipszie - 1 + shipszie - 2 + shipszie - 3 + shipszie - 4;
                bombb = bomba;
                label3.Text = bomba.ToString(); label4.Text = bombb.ToString();
                r = new Button[amsh, amsh];
                n = new int[amsh, amsh];
                for (int i = 0; i < r.GetLength(0); i++)
                {
                    for (int x = 0; x < r.GetLength(1); x++)
                    {
                        n[i, x] = new int();
                        r[i, x] = new Button();
                        r[i, x].Size = new Size(panel1.Width / r.GetLength(0), panel1.Height / r.GetLength(1));
                        r[i, x].BackColor = Color.Blue;
                        r[i, x].FlatStyle = FlatStyle.Flat;
                        n[i, x] = 0;
                        r[i, x].Location = new Point(r[i, x].Width * x, r[i, x].Height * i);
                        if (x != 0)
                        {
                            if (x == amsh / 2)
                            {
                                r[i, x].BackColor = Color.Green;
                                n[i, x] = 5;
                            }
                        }
                        r[i, x].Tag = counter;
                        r[i, x].Click += Form1_Click;
                        panel1.Controls.Add(r[i, x]);
                        radioButton1.Visible = true;
                        radioButton2.Visible = true;
                        counter++;
                    }
                }
            }
        }

        #region מציג מערך
        private void button2_Click(object sender, EventArgs e)
        {
            string array = "";

            for (int y = 0; y < r.GetLength(0); y++)
            {
                for (int x = 0; x < r.GetLength(1); x++)
                {
                    array += n[y, x] + ", ";
                }
                array += "\n";
            }
            MessageBox.Show(array);
        }
        #endregion

        #region השחקן בוחר אם לצד או לגובהה
        private void radioButton1_CheckedChanged_1(object sender, EventArgs e)
        {
            sides = 1;
            amuda = 0;
        }
        private void radioButton2_CheckedChanged(object sender, EventArgs e)
        {
            amuda = 1;
            sides = 0;
        }
        #endregion

        #region כשלוחצים על סיום הצבת הספינות ועובר משחקן א ל ב
        private void button3_Click(object sender, EventArgs e)
        {            
            player = 1;// התור של שחקן ב
            button3.Visible = false;
            if (againstcom)
            {
                radioButton1.Visible = false;//מעלים את האפשרות לבחור שורה
                radioButton2.Visible = false;//מעלים את האפשרות לבחור עמודה - כי זה נגד המחשב אז אין צורך בזה
                shipszie = int.Parse(textBox2.Text);// שגדול הספינה שוב יהיה שווה לגודל המקורי- בשחקן אלף הגודל ירד

                for (integer = shipszie; integer >= (int.Parse(textBox2.Text) > 5 ? 1 : 0); integer--)
                {
                    int rand = ship.Next(2);
                    sides = (rand == 1) ? 1 : 0;
                    amuda = (rand == 1) ? 0 : 1;
                    if (sides == 1)
                    {
                        Y = ship.Next(r.GetLength(0));
                        X = ship.Next((shipszie > 1) ? r.GetLength(0) / 2 + shipszie / 2 : r.GetLength(0) / 2 + 1, r.GetLength(0) - shipszie / 2);
                    }
                    else
                    {
                        Y = ship.Next((shipszie > 1) ? shipszie / 2 : 1, r.GetLength(0) - shipszie / 2);
                        X = ship.Next(r.GetLength(0) / 2 + 1, r.GetLength(0));
                    }

                    PlayerBShips();
                }
            }
            else
            if (tor == (int.Parse(textBox2.Text) > 4 ? 5 : 4))
            {
                shipszie = int.Parse(textBox2.Text);
                for (int a = 0; a < r.GetLength(0); a++)
                {
                    for (int s = 0; s < r.GetLength(1); s++)
                    {
                        if (s < r.GetLength(0) / 2)
                            r[a, s].BackColor = Color.Blue;
                    }
                }
            }
        }
        #endregion

        #region כשלוחצים על התחל מתקפה
        private void button5_Click(object sender, EventArgs e)
        {
            tor = -1; amuda = 0; sides = 0;

            for (int a = 0; a < r.GetLength(0); a++)
            {
                for (int s = 0; s < r.GetLength(1); s++)
                {
                    if (s > r.GetLength(0) / 2)
                        r[a, s].BackColor = Color.Blue;
                }
            }
            button5.Visible = false;
        
        }
        #endregion

        #region פונקציות שקוראת להצבת ספינות
        void PlayerAShips()
        {
            if (sides == 1)
            {
                if (X + shipszie / 2 < r.GetLength(1) / 2 && X - ((shipszie - 1) / 2) >= 0 && !(int.Parse(textBox2.Text) == 6 && tor == 5))
                {
                    PutShips(false);
                }
            }
            if(amuda==1)
            {
                if (Y + shipszie / 2 < r.GetLength(1) - 1 / 2 && Y - ((shipszie - 1) / 2) >= 0 && player == 0 && !(int.Parse(textBox2.Text) == 6 && tor == 5))
                    PutShips(true);
            }
        }

        void PlayerBShips()
        {
            if (sides == 1)
            {
                if (X - ((shipszie - 1) / 2) > r.GetLength(0) / 2 && X + shipszie / 2 < r.GetLength(0) && !(tor >= (int.Parse(textBox2.Text) > 4 ? 10 : 8)))
                {
                    PutShips(false);
                }
            }
            if (amuda == 1)
            {
                if (Y - ((shipszie - 1) / 2) > 0 && Y + shipszie / 2 < r.GetLength(0) && !(tor >= (int.Parse(textBox2.Text) > 4 ? 10 : 8)))
                    PutShips(true);
            }
        }
        #endregion

        #region הצבת ספינות 
        void PutShips(bool vertical)
        {
                label1.Visible = false; label2.Visible = false;
                textBox1.Visible = false; textBox2.Visible = false;
                List<string> used = new List<string>();
                for (int i = -((shipszie - 1) / 2); i < shipszie - ((shipszie - 1) / 2); i++)
                {
                    int y = Y + (vertical ? i : 0);// אם זה מאונך שיוסיף לוואי
                    int x = X + (vertical ? 0 : i);// אם זה לא מאונך שיוסיף לאיקס
                    if (n[y, x] != 2)
                    {
                        if ((player == 1 && !againstcom) || player == 0)
                            r[y, x].BackColor = Color.Black;
                        n[y, x] = 2;
                        used.Add(y + " " + x);
                    }
                    else
                    {
                        foreach (var str in used)
                        {
                            string[] a = str.Split(' ');
                            n[int.Parse(a[0]), int.Parse(a[1])] = 0;
                            if (!againstcom || player == 0)
                                r[int.Parse(a[0]), int.Parse(a[1])].BackColor = Color.Blue;
                        }
                        if (!againstcom || player == 0)
                            MessageBox.Show("Please select a different place and the ships may not be on top of each other");
                        else
                            integer++;
                        shipszie++;
                        tor--;
                        break;
                    }
                }
                if (tor == (int.Parse(textBox2.Text) > 4 ? 4 : 3))
                    button3.Visible = true;
                if (tor == (int.Parse(textBox2.Text) > 4 ? 9 : 7))
                    button5.Visible = true;
                shipszie--;
                tor++;
        }


        #endregion
      
        #region התקפות מחשב ושחקן ב
        void AttackB()
        {
            #region Two players
            if (!againstcom)
            {
                if (n[Y, X] == 2)
                {
                    r[Y, X].BackColor = Color.Red;
                    n[Y, X] = 3;
                    BackColor = Color.Red;
                    bombb--;
                    label3.Text = bombb.ToString(); label4.Text = bomba.ToString();
                }
                else
                {
                    r[Y, X].BackColor = Color.Yellow;
                    n[Y, X] = 4;
                    BackColor = Color.Yellow;
                }
                whonow++;
                label7.Text = "Attack of player : A";
            }
            #endregion

            else // של התנאי אם זה נגד המחשב שנמצא בתוך הריג'ן מעל
            {
                if (lastHitX < 0)// אם לא היה עדיין פגיעה
                    RandomPlace();
                else
                {
                    if (n[lastHitY, lastHitX] == 3)// אם היה פגיעה
                    {
                        if (lastHitY + verti >= 0 && lastHitY + verti < r.GetLength(0) && lastHitX + hori >= 0 && lastHitX + hori < r.GetLength(0) / 2)// גבולות
                        {
                            if (n[lastHitY + verti, lastHitX + hori] == 2)// אם במקום שרוצה לתקוף יש ספינה
                            {
                                Fire(lastHitY + verti, lastHitX + hori);// שיתקוף

                                if (hori == 0)
                                    verti += (verti < 0) ? -1 : 1;
                                else
                                    hori += (hori < 0) ? -1 : 1;
                            }

                            else
                            {
                                bool ok = n[lastHitY + verti, lastHitX + hori] > 2;
                                if (!ok)// אם לא היה בכלל פגיעות באותו מקום
                                    Fire(lastHitY + verti, lastHitX + hori);// שיתקיף

                                ResumeFight(ok);

                                if (ok) return;
                            }

                        }
                        else
                        {
                            ResumeFight(true);
                            return;
                        }
                    }
                    else
                        RandomPlace();
                }

                if (bombb == 0)
                {
                    MessageBox.Show("B player's victory");
                    button6.Visible = false; 
                    tor = 0;newGame = -1;hori = -1; verti = 0;
                }
                whonow++;
                label7.Text = "Attack of player : A";
            }
        }
        
        void ResumeFight(bool ok)
        {
            if (Math.Abs/*ערך מוחלט הופך ממינוס לפלוס*/(hori) < 2 && Math.Abs(verti) < 2)
            {
                if (verti == 1)
                    lastHitX = -1;

                verti = hori;
                hori = hori == 0 ? 1 : 0;

                if (ok)
                {
                    AttackB();
                    return;
                }
            }
            else
            {
                if (hori <= 0 && verti <= 0)
                {
                    if (hori == 0)
                        verti = 1;
                    else
                        hori = 1;

                    if (ok)
                    {
                        AttackB();
                        return;
                    }
                }
                else
                {
                    hori = -1; verti = 0;
                    lastHitX = -1;

                    if (ok)
                    {
                        AttackB();
                        return;
                    }
                }
            }
        }

        void RandomPlace()
        {
            do //it will do the loop at least one time and then it will see the while condition, try to look at the loop in break mode and hen ask me.
            {
                Y = ship.Next(r.GetLength(0));
                X = ship.Next(r.GetLength(0) / 2);
                lastHitY = Y; lastHitX = X;
            }
            while (n[Y, X] > 2);// שאם היה כבר פגיעה אז שיחזור
            Fire(Y, X);
        }

        void Fire(int ln, int col)
        {
            if (n[ln, col] == 2)
            {
                r[ln, col].BackColor = Color.Red;
                n[ln, col] = 3;
                BackColor = Color.Red;
                bombb--;
                label3.Text = bombb.ToString(); label4.Text = bomba.ToString();
            }
            else
            {
                r[ln, col].BackColor = Color.Yellow;
                BackColor = Color.Yellow;
                n[ln, col] = 4;
            }
        }
        #endregion

        private void button6_Click(object sender, EventArgs e)
        {
            if (againstcom && tor == -1 && whonow % 2 != 0)
            {
                AttackB();
            }
        }

        void Form1_Click(object sender, EventArgs e)
        {
            int Loc = (int)((Button)sender).Tag; // מקבל את הטאג של הלחצן שלחצת
            Y = Loc / r.GetLength(1); X = Loc % r.GetLength(1);// מיקומים על פי הטאג 

            #region הצבת הספינות
            if (tor >= 0)
            {
                #region שחקן א
                if (X < r.GetLength(1) / 2 && tor < int.Parse(textBox2.Text))
                {
                    PlayerAShips();
                }
                if (tor == (int.Parse(textBox2.Text) > 4 ? 4 : 3))
                {
                    radioButton1.Checked = false;
                    radioButton2.Checked = false;
                }
                #endregion

                #region שחקן ב
                if (player == 1 && X > r.GetLength(0) / 2 && X < r.GetLength(1))
                {
                    PlayerBShips();
                }
                #endregion
            }
            #endregion

            #region התקפה של שחקן א על ב
            if (tor == -1 && X > r.GetLength(0) / 2 && amuda == 0 && sides == 0 && whonow % 2 == 0 && n[Y, X] != 3 && n[Y, X] != 4)
            {
                label7.Text = "Attack of player :  A";
                if (n[Y, X] == 2)
                {
                    if (!againstcom)
                    {
                        r[Y, X].BackColor = Color.Red;
                        this.BackColor = Color.Red;
                    }
                    else
                    {
                        r[Y, X].BackColor = Color.Red;
                        this.BackColor = Color.Lime;
                    }
                    n[Y, X] = 3;
                    bomba--;
                    label3.Text = bombb.ToString(); label4.Text = bomba.ToString();
                }
                else
                {
                    r[Y, X].BackColor = Color.Yellow;
                    this.BackColor = Color.Yellow;
                    n[Y, X] = 4;
                }

                if (bomba == 0)
                {
                    MessageBox.Show("A player's victory");
                    button6.Visible = false;
                    tor = 0; newGame = -1;
                }

                whonow++;
                label7.Text = "Attack of player : B";
                button6.Focus();              
            }
            #endregion

            #region התקפה של שחקן ב על א
            
            if (tor == -1 & X < r.GetLength(0) / 2 && whonow % 2 != 0 && n[Y, X] != 3 && n[Y, X] != 4&&!againstcom)
            {
                AttackB();
            }
            #endregion

        }
    }
}
