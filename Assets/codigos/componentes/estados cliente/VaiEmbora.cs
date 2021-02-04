﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NhomNhom {

    public class VaiEmbora : MonoBehaviour
    {
        public Transform ptFolha;

        Cadeiras cadeiras;
        Transform[] pontos;
        int i_ponto;
        ControladorVaiAtePonto ctrlVaiAtePonto;
        ControleCliente ctrlCliente;
        PagamentoFeedback pagFeedback;

        public bool FoiEmbora() {
            return i_ponto == pontos.Length - 1 && ctrlVaiAtePonto.estaNoPonto;
        }

        void Awake() {
            cadeiras = FindObjectOfType<Cadeiras>();
            ctrlVaiAtePonto = GetComponent<ControladorVaiAtePonto>();
            ctrlCliente = GetComponent<ControleCliente>();
            pagFeedback = GetComponent<PagamentoFeedback>();
        }

        void Start() {
            ptFolha = GetComponent<ControleCliente>().ptCadeira;

            pontos = cadeiras.ObterRotaSaida(ptFolha);

            ctrlVaiAtePonto.trAlvo = pontos[i_ponto];
            ctrlVaiAtePonto.ativo = true;

            FindObjectOfType<Cadeiras>().AbrirVaga(ptFolha);

            Paciencia paciencia = GetComponent<Paciencia>();

            if (paciencia.paciencia > 0f) {
                EstadosCliente estado = GetComponent<EstadosCliente>();
                int recompensa = GetComponent<Recompensa>().ObterRecompensa(estado.precoPrato);
                Cofre cofre = FindObjectOfType<Cofre>();
                cofre.Pagar(recompensa);
                pagFeedback.Iniciar(paciencia.ObterMarca());
            }

            ctrlCliente.anim.SetBool("movimento", true);
        }

        void Update() {
            if (SistemaPausa.pausado)
                return;

            if (ctrlVaiAtePonto.estaNoPonto && !FoiEmbora() && i_ponto < pontos.Length - 1) {
                i_ponto++;
                ctrlVaiAtePonto.trAlvo = pontos[i_ponto];
                ctrlVaiAtePonto.estaNoPonto = false;
            }
        }
    }
}
