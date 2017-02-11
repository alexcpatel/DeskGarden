using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class Growth : MonoBehaviour {

	static GameObject current_selected;

    private int waterCount;
    private float growthTimeLimit;
    private bool growing;
    private float growthFactor;
    private float growthTime;
    private bool plantable;
    private float growthHeightFactor;

    public GameObject plant;
    public GameObject sunflower;
    public GameObject cactus;
    public GameObject rose;
    public GameObject bonsai;
    public GameObject grass;
    public GameObject pot;
    public GameObject dirtobject;
    public GameObject sprinkleobject;

    public Canvas canvas;

    // Plant List
    //
    // Plant
    // Sunflower
    // Cactus
    // Rose
    // Bonsai Tree
    // Grass
    //

    static Mesh plantModel;
    static Mesh sunflowerModel;
	static Mesh cactusModel;
    static Mesh roseModel;
    static Mesh bonsaiModel;
    static Mesh grassModel;

    static Material plantMaterial;
    static Material sunflowerMaterial;
    static Material cactusMaterial;
    static Material roseMaterial;
    static Material bonsaiMaterial;
    static Material grassMaterial;

    static float plantHeight;
    static float sunflowerHeight;
    static float cactusHeight;
    static float roseHeight;
    static float bonsaiHeight;
    static float grassHeight;

    static float plantScale;
    static float sunflowerScale;
    static float cactusScale;
    static float roseScale;
    static float bonsaiScale;
    static float grassScale;

    static float plantGrowth;
    static float sunflowerGrowth;
    static float cactusGrowth;
    static float roseGrowth;
    static float bonsaiGrowth;
    static float grassGrowth;

    private ParticleSystem dirt;
    private ParticleSystem sprinkle;

    public int buttonNumber;

    // Use this for initialization
    void Start () {

        gameObject.GetComponent<MeshRenderer>().enabled = false;
        plantable = true;
        waterCount = 0;
        growing = false;
        growthTimeLimit = 10.0f;
        growthFactor = 0.2f;
        growthHeightFactor = 0.1f;
        growthTime = 0.0f;

        plantModel = plant.GetComponent<MeshFilter>().mesh;
        sunflowerModel = sunflower.GetComponent<MeshFilter>().mesh;	
        cactusModel = cactus.GetComponent<MeshFilter>().mesh;
        roseModel = rose.GetComponent<MeshFilter>().mesh;
		bonsaiModel = bonsai.GetComponent<MeshFilter>().mesh;
		grassModel = grass.GetComponent<MeshFilter>().mesh;

        plantMaterial = plant.GetComponent<Renderer>().material;
        sunflowerMaterial = sunflower.GetComponent<Renderer>().material;
        cactusMaterial = cactus.GetComponent<Renderer>().material;
        roseMaterial = rose.GetComponent<Renderer>().material;
        bonsaiMaterial = bonsai.GetComponent<Renderer>().material;
        grassMaterial = grass.GetComponent<Renderer>().material;

        plantHeight = 0.01f;
        sunflowerHeight = 0.13f;
        cactusHeight = 0.01f;
        roseHeight = -0.05f;
        bonsaiHeight = 0.001f;
        grassHeight = 0.005f;

        plantScale = 0.04f;
        sunflowerScale = 0.04f;
        cactusScale = 0.04f;
        roseScale = 0.001f;
        bonsaiScale = 0.001f;
        grassScale = 0.04f;

        plantGrowth = 0.2f;
        sunflowerGrowth = 0.2f;
        cactusGrowth = 0.2f;
        roseGrowth = 0.005f;
        bonsaiGrowth = 0.03f;
        grassGrowth = 0.2f;

        dirt = dirtobject.GetComponent<ParticleSystem>();
        sprinkle = sprinkleobject.GetComponent<ParticleSystem>();
        dirt.Stop();
        sprinkle.Stop();
    }

    void digUp ()
    {
        dirt.Play();
        gameObject.GetComponent<MeshRenderer>().enabled = false;
        plantable = true;
        growing = false;
		waterCount = 0;
    }

    IEnumerator grow()
    {
        yield return new WaitForSeconds(1.7f);
        growing = true;
        yield return null;
    }

    void water () //Waters plant - initiates growth if not at Stage 3
    {
        sprinkle.Play();
        if (waterCount < 2)
        { 
            waterCount++;
            StartCoroutine("grow");
        }
    }

    void clicked ()
    {
        if (buttonNumber == 0)
        {
            digUp();
        }
        if (gameObject.activeSelf && buttonNumber == 1 && growing == false)
        {
            water();
        }
		if (plantable && 2 <= buttonNumber)
        {
            switch (buttonNumber)
            {
                case 2:
					setMeshElements(plantModel, plantMaterial);
                    transform.localScale = new Vector3(plantScale, plantScale, plantScale);
                    transform.localPosition = new Vector3(transform.localPosition.x, 0.1406f,transform.localPosition.z);
                    growthHeightFactor = plantHeight;
                    growthFactor = plantGrowth;
                    break;
                case 3:
					setMeshElements(sunflowerModel, sunflowerMaterial);
                    transform.localScale = new Vector3(sunflowerScale, sunflowerScale, sunflowerScale);
                    transform.localPosition = new Vector3(transform.localPosition.x, 0.1406f, transform.localPosition.z);
                    growthHeightFactor = sunflowerHeight;
                    growthFactor = sunflowerGrowth;                   
                    break;
                case 4:
					setMeshElements(cactusModel, cactusMaterial);
                    transform.localScale = new Vector3(cactusScale, cactusScale, cactusScale);
                    transform.localPosition = new Vector3(transform.localPosition.x, 0.1406f, transform.localPosition.z);
                    growthHeightFactor = cactusHeight;
                    growthFactor = cactusGrowth;
                    break;
                case 5:
					setMeshElements(roseModel, roseMaterial);
                    transform.localScale = new Vector3(roseScale, roseScale, roseScale);
                    transform.localPosition = new Vector3(transform.localPosition.x, 0.1406f, transform.localPosition.z);
                    growthHeightFactor = roseHeight;
                    growthFactor = roseGrowth;
                    break;
                case 6:
					setMeshElements(bonsaiModel, bonsaiMaterial);
                    transform.localScale = new Vector3(bonsaiScale, bonsaiScale, bonsaiScale);
                    transform.localPosition = new Vector3(transform.localPosition.x, 0.1406f, transform.localPosition.z);
                    growthHeightFactor = bonsaiHeight;
                    growthFactor = bonsaiGrowth;
                    break;
                case 7:
					setMeshElements(grassModel, grassMaterial);
                    transform.localScale = new Vector3(grassScale, grassScale, grassScale);
                    transform.localPosition = new Vector3(transform.localPosition.x, 0.1406f, transform.localPosition.z);
                    growthHeightFactor = grassHeight;
                    growthFactor = grassGrowth;
                    break;
            }
			gameObject.GetComponent<MeshRenderer> ().enabled = true;
            plantable = false;
			growing = false;
        }
    }

	void setMeshElements(Mesh mesh, Material material) {
		gameObject.GetComponent<MeshFilter> ().mesh = mesh;
		gameObject.GetComponent<MeshCollider> ().sharedMesh = mesh;
        gameObject.GetComponent<Renderer>().material = material;
	}

	void getTouch() {
		if (Input.GetMouseButtonDown(0))
			//Input.GetMouseButtonDown(0)
			//Input.touchCount > 0
		{
            /*
            if (EventSystem.current.IsPointerOverGameObject (Input.GetTouch (0).fingerId)) {
				//UIPlantControl.data_text.text = "cancelling";
				return;
			}*/

			RaycastHit hit;
			Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
			//Input.mousePosition
			//Input.GetTouch(0).position
			if (Physics.Raycast(ray, out hit, Mathf.Infinity))
			{
				UIPlantControl.data_text.text = hit.collider.gameObject.name;
				if (hit.collider.gameObject == gameObject || hit.collider.gameObject == pot) {
                    if (UIPlantControl.info_mode)
                    {
                        if (hit.collider.gameObject == gameObject) { 
                        canvas.GetComponent<UIPlantControl>().activateMoreInfoPanel(gameObject.GetComponent<MeshFilter>().mesh.name);
                        }
                    } else {
                        current_selected = gameObject;
                        buttonNumber = UIPlantControl.plant_chosen;
                        UIPlantControl.data_text.text = buttonNumber.ToString();
                        clicked();
                    }
				}
			}
		}
	}

	// Update is called once per frame
	void Update ()
    {
		getTouch ();

        if (growing)
        {
            float dT = Time.deltaTime / growthTimeLimit;
            Vector3 v1 = new Vector3(growthFactor * dT / 1.3f, growthFactor * dT, growthFactor * dT / 1.3f);
            transform.localScale += v1;
            Vector3 v2 = new Vector3(0,growthHeightFactor * dT,0);
            transform.localPosition += v2;

            growthTime += Time.deltaTime;
            if (growthTime >= growthTimeLimit)
            {
                growing = false;
                growthTime = 0.0f;
            }
        }
	}
}
