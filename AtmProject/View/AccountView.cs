﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;
using AtmProject.Banco;
using AtmProject.Entidades;
namespace AtmProject
{
    public partial class AccountView : Form
    {
        public AccountView()
        {
            InitializeComponent();

        }

        private void btn_sign_in_Click(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(tb_num_conta.Text) ||
                string.IsNullOrWhiteSpace(tb_pin.Text) ||
                string.IsNullOrWhiteSpace(tb_endereco.Text) ||
                string.IsNullOrWhiteSpace(tb_nome.Text) ||
                string.IsNullOrWhiteSpace(tb_telefone.Text) ||
                cb_educacao.SelectedItem == null ||
                string.IsNullOrWhiteSpace(dt_nascimento.Value.ToString("yyyy-MM-dd")))
            {
                MessageBox.Show("Preencha o formulário.");
                return;
            }
            if (!Util.ValidateAccountNumber(tb_num_conta.Text))
            {
                MessageBox.Show("Número da conta inválido. Deve conter exatamente 5 dígitos.");
                return;
            }

            if (!Util.ValidatePin(tb_pin.Text))
            {
                MessageBox.Show("PIN inválido. Deve conter exatamente 5 dígitos.");
                return;
            }

            if (!Util.ValidateName(tb_nome.Text))
            {
                MessageBox.Show("Nome inválido. Deve conter apenas letras.");
                return;
            }

            if (!Util.ValidatePhoneNumber(tb_telefone.Text))
            {
                MessageBox.Show("Número de telefone inválido. ");
                return;
            }

            if (!Util.ValidateAddress(tb_endereco.Text))
            {
                MessageBox.Show("Endereço não pode estar vazio.");
                return;
            }

            if (!Util.ValidateEducation(cb_educacao.SelectedItem.ToString()))
            {
                MessageBox.Show("Educação inválida. Selecione uma opção.");
                return;
            }

            if (!Util.ValidateBirthDate(dt_nascimento.Value))
            {
                MessageBox.Show("Data de nascimento inválida.");
                return;
            }

            if (!Util.IsOver18YearsOld(dt_nascimento.Value))
            {
                MessageBox.Show("Você deve ter 18 anos ou mais para se cadastrar.");
                return;
            }

            try
            {
                Account account = new Account();

                // Adiciona os parâmetros ao comando
                account.AccNum = Convert.ToInt32(tb_num_conta.Text);
                account.Name = tb_nome.Text;
                account.Phone = tb_telefone.Text;
                account.Address = tb_endereco.Text;
                account.Education = cb_educacao.SelectedItem.ToString();
                account.Dob = dt_nascimento.Value;
                account.Pin = Convert.ToInt32(tb_pin.Text);


                Repositorio.AccountRepository accountRepository = new Repositorio.AccountRepository();
                accountRepository.Create(account);

                MessageBox.Show("Sua conta foi criada com sucesso!");
                LoginView log = new LoginView();
                log.Show();
                this.Hide();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void lbl_log_out_Click(object sender, EventArgs e)
        {
            LoginView log = new LoginView();
            log.Show();
            this.Hide();
        }

        private void lbl_sair_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void account_Load(object sender, EventArgs e)
        {
            cb_educacao.SelectedIndex = 0;

        }
    }
}