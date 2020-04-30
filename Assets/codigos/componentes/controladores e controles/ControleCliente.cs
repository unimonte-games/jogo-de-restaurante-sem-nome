﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NhomNhom {

    public class ControleCliente : MonoBehaviour
    {
        public Transform ptCadeira;
        public int id;

        public OlhadorSuave olhador;

        Transform tr;
        Controle ctrl;
        Velocidade compVelocidade;
        RotacionadorSuave rotSuave;

        Vector3 direcao;

        public void OlharPonto(Vector3 pontoOlhar)
        {
            if (olhador.rotSuave.atualizar) {
                Quaternion rotBkup = olhador.rotSuave.tr.rotation;
                tr.LookAt(pontoOlhar);
                olhador.alvo = pontoOlhar;
                olhador.rotSuave.tr.rotation = rotBkup;
            } else
                tr.LookAt(pontoOlhar);
        }

        void Awake() {
            compVelocidade = GetComponent<Velocidade>();
            tr = GetComponent<Transform>();
            ctrl = GetComponent<Controle>();
            rotSuave = GetComponent<RotacionadorSuave>();
        }

        void Update() {
            float H = ctrl.ctrlValores.eixoHorizontal;
            float V = ctrl.ctrlValores.eixoVertical;

            direcao.x = H;
            direcao.y = 0;
            direcao.z = V;

            if (direcao.magnitude > 1f)
                direcao.Normalize();

            compVelocidade.direcao.x = 0;
            compVelocidade.direcao.y = 0;

            if (direcao.magnitude > 0.1f) {
                OlharPonto(tr.position + direcao);

                compVelocidade.direcao.z = Mathf.Ceil(direcao.magnitude);
                compVelocidade.velocidade = ctrl.velocidade;
            } else {
                compVelocidade.direcao.z = 0;
                compVelocidade.velocidade = 0;
            }
        }
    }
}
