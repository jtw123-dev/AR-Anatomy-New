using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Amazon.S3;
using Amazon;
using Amazon.CognitoIdentity;
using Amazon.S3.Model;
using System.IO;
public class AWSManager : MonoBehaviour
{
    private static AWSManager _instance;
    public static AWSManager Instance    
    {
        get
        {
            if (_instance==null)
            {
                Debug.LogError("AWS Manager is null");
            }
            return _instance;
        }
    }

    [SerializeField] private GameObject _targetImg;
    public string S3Region = RegionEndpoint.CACentral1.SystemName;
    private RegionEndpoint _S3Region
    {
        get { return RegionEndpoint.GetBySystemName(S3Region); }
    }

    private AmazonS3Client _s3Client;
    public AmazonS3Client S3Client
    {
        get
        {
            if(_s3Client==null)
            {
                _s3Client = new AmazonS3Client(new CognitoAWSCredentials("ca-central-1:d0a1c202-787a-4302-bd38-36ae9290fb2d",//make sure to change to service app new
                    RegionEndpoint.CACentral1),_S3Region);
            }
            return _s3Client;
        }
    }

    private void Awake()
    {
        _instance = this;
        UnityInitializer.AttachToGameObject(this.gameObject);
        AWSConfigs.HttpClient = AWSConfigs.HttpClientOption.UnityWebRequest;

        var request = new ListObjectsRequest()
        {
            BucketName = "arbundlesnew"
        };

        S3Client.ListObjectsAsync(request, (responseobj) =>
        {
            if (responseobj.Exception==null)
            {
                responseobj.Response.S3Objects.ForEach((obj) =>
                {
                    Debug.Log("Obj: " + obj.Key);
                });
            }
            else
            {
                Debug.LogWarning("Response Exception: " + responseobj.Exception);
            }

        });

        DownloadBundle();
    }

    public void DownloadBundle()
    {
        /* string bucketName = "arbundlesnew";
         string fileName = "horse";

         S3Client.GetObjectAsync(bucketName, fileName, (responseObj) =>
              {
                  if (responseObj.Exception == null)
                  {
                      string data = null;
                      using (StreamReader reader = new StreamReader(responseObj.Response.ResponseStream))
                      {
                          data = reader.ReadToEnd();
                          Debug.Log("Data" + data);
                      }

                  }
             });*/
        StartCoroutine(BundleRoutine());
    }
    IEnumerator BundleRoutine()
    {
        string url = "https://arbundlesnew.s3.ca-central-1.amazonaws.com/horse"; //https://arbundlesnew.s3.ca-central-1.amazonaws.com/horse
        var request = new WWW(url);
        yield return request;

        Debug.Log("Asset Bundle: " + request.assetBundle.name);
        AssetBundle bundle = request.assetBundle;
        GameObject horse = bundle.LoadAsset<GameObject>("horse");
       horse= Instantiate(horse);

        horse.transform.parent = _targetImg.transform;
    }
}
