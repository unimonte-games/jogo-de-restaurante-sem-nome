﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NhomNhom {

    public class ComendoPrato : MonoBehaviour {
        float tempoInicio, intervaloPrato;
        bool terminou = false;

        void Start() {
            tempoInicio = Time.time;

            EspacoItem espacoItem = transform.Find("ref_item").GetComponent<EspacoItem>();
            Item itemPrato = espacoItem.itemAbrigado;

            Prato prato = itemPrato.GetComponent<Prato>();
            intervaloPrato = prato.tempoConsumo;
            GetComponent<EstadosCliente>().precoPrato = prato.ObtemPreco();

            GetComponent<BolhaCliente>().Ocultar();

            StartCoroutine(ConsumirEDestruir(espacoItem, intervaloPrato));
        }

        IEnumerator ConsumirEDestruir(EspacoItem espacoItem, float intervaloPrato) {
            Paciencia paciencia = GetComponent<Paciencia>();
            paciencia.consumir = false;
            // paciencia.Recuperar();

            yield return new WaitForSeconds(intervaloPrato);

            espacoItem.itemAbrigado.bloqueado = false;
            Item itemPrato = espacoItem.Soltar();
            Destroy(itemPrato.gameObject);

            terminou = true;
        }

        public bool Comeu() {
            return terminou;
        }
    }
}
