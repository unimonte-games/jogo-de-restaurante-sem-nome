﻿using System;
﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace NhomNhom {

    public class Recompensa : MonoBehaviour {
        public float[] recompensas;
        Paciencia paciencia;

        void Awake () {
            paciencia = GetComponent<Paciencia>();
        }

        public int ObterRecompensa(int precoBase) {
            return Mathf.FloorToInt(precoBase * recompensas[paciencia.ObterMarca()]);
        }

//    #if UNITY_EDITOR
//        void OnValidate() {
//            Paciencia _paciencia = GetComponent<Paciencia>();
//
//            if (!_paciencia)
//                return;
//            else if (recompensas.Length != _paciencia.divisoes.Length+1)
//                Debug.LogWarning("a quantidade de recompensas não bate com a quantidade de marcas de paciência", gameObject);
//        }
//    #endif
    }
}
