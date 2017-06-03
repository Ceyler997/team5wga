using UnityEngine;

[RequireComponent(typeof(SpriteRenderer))]
public class TokenUI : MonoBehaviour {

    public BaseObject target;
    public Color friendColor = new Color(0, 0, 1);
    public Color neutralColor = new Color(0.7f, 0.7f, 0.7f);
    public Color enemyColor = new Color(1, 0, 0);

    private SpriteRenderer tokenRenderer;
    private Player controllingPlayer;

	void Start () {
        tokenRenderer = GetComponent<SpriteRenderer>();
        if(tokenRenderer != null) {
            tokenRenderer.color = neutralColor;
        }

        if(target == null) {
            target = GetComponentInParent<BaseObject>();
        }
	}
	
	void Update () {
        if(tokenRenderer == null || target == null) {
            return;
        }

        if(target.ControllingPlayer == controllingPlayer) {
            return;
        }

        controllingPlayer = target.ControllingPlayer;

        if(controllingPlayer == null) {
            tokenRenderer.color = neutralColor;
        } else if (controllingPlayer.ID == PhotonNetwork.player.ID) {
            tokenRenderer.color = friendColor;
        } else {
            tokenRenderer.color = enemyColor;
        }
	}
}
