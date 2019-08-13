using System.Collections;
using System.Collections.Generic;
using UnityEngine;

using UnityEngine.TestTools;
using NUnit.Framework;

public class TestSuite
{
    private GameObject game;
    private GameManager gameManager;
    private Player player;
    private int oldScore;
    private int newScore;
    public bool isGrounded = false;
  
    [SetUp]
    public void Setup()
    {
        GameObject prefab = Resources.Load<GameObject>("Prefabs/Game");
        game = Object.Instantiate(prefab);
        // Get GameManager
        gameManager = GameManager.Instance;
        //player = Object.FindObjectOfType<Player>();
        player = game.GetComponentInChildren<Player>(); 

    }

    [UnityTest]
    public IEnumerator GamePrefabLoaded()
    {
        yield return new WaitForEndOfFrame();

        //Game object should exist at this point in time
        Assert.NotNull(game);
    }

    public IEnumerator PlayerExists()
    {
        yield return new WaitForEndOfFrame();

        Assert.NotNull(player, "Player dead come again");
    }

    [UnityTest]
    public IEnumerator ItemCollideWithPlayer()
    {
        //Item item = gameManager.itemManager.GetItem(0);
        //item.transform.position = player.transform.position;


        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Entities/Item");

        Vector3 playerPosition = player.transform.position;
        GameObject item = Object.Instantiate(itemPrefab, playerPosition, Quaternion.identity);

        yield return new WaitForSeconds(.1f);

        Assert.IsTrue(item == null);
    }

    [UnityTest]
    public IEnumerator ItemCollectAndScoreAdded()
    {
        // Span an item (same as above)
        GameObject itemPrefab = Resources.Load<GameObject>("Prefabs/Entities/Item");
        // Record old score in an int
        int oldScore = gameManager.score;
        // WaitForFixedUpdate
        yield return new WaitForFixedUpdate();
        // WaitForEndOfFrame
        yield return new WaitForEndOfFrame();

        // Assert IsTrue old score != new score
        Assert.IsTrue(oldScore != newScore);
    }

    [UnityTest]
    public IEnumerator IsGrounded()
    {
        // WaitForFixedUpdate
        yield return new WaitForFixedUpdate();

        if (isGrounded)
        {
            
        }

        // WaitForEndOfFrame
        yield return new WaitForEndOfFrame();


        // Assert IsTrue old score != new score
        Assert.IsTrue(oldScore != newScore);
    }

    [TearDown]
    public void TearDown()
    {
        Object.Destroy(game);
    }
}
