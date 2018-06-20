using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using SOPRO;
public class CoinCountersResetter : MonoBehaviour {

    public SOVariableUint CoinsPicked;
    public SOVariableUint CoinsMissed;
    public SOVariableUint CoinsPickedValue;
    public SOVariableUint CoinsMissedValue;
    void Start () {

        CoinsPicked.Value = 0;
        CoinsMissed.Value = 0;
        CoinsPickedValue.Value = 0;
        CoinsMissedValue.Value = 0;
        Destroy(this);
	}
}
