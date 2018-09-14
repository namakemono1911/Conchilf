using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playerDefault : playerState {

    public playerDefault(playerController p) : base(p) { }

	// Use this for initialization
	void Start () {
		
	}

    public override void initState()
    {

    }

    public override void updateState()
    {
        Vector3 reticlePos = Vector3.zero;

        reticlePos.x = Input.GetAxis(player.Control.axisNameX) * player.Control.mouseSensitivity;
        reticlePos.y = Input.GetAxis(player.Control.axisNameY) * player.Control.mouseSensitivity;

        player.Control.reticle.position += reticlePos;
    }
}
