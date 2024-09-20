using System.Collections;
using System.Collections.Generic;
using System.Xml.Serialization;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class Customer : MonoBehaviour
{
    public enum State { None, Sit, Order, Eat, Exit }
    State curState;
    public Seat mySeat;
    FSM fsm;
    MyEnum.FoodType myOrder;
    public MyEnum.FoodType MyOrder { get { return myOrder; } }

    public Transform exit;

    public float speed = 0.05f;

    [Header("Order Bubble")]
    [SerializeField] public GameObject orderBubble;
    [SerializeField] public TMP_Text orderBubbleText;
    [SerializeField] public CompleteFood orderFood;

    void Start()
    {
        exit = GameObject.FindWithTag("Exit").transform;

        mySeat = GameObject.FindWithTag("Seat").GetComponent<Seat>();
        mySeat.OnFoodReadied += IsReceiveMyOrder;

        fsm = new FSM(new SitState(this));

        myOrder = ChoiceMyOrder();

        curState = State.Sit;
        ChangeState(State.Sit);
    }

    private void Update()
    {
        switch (curState)
        {
            case State.Sit:
                if (IsNearSeat())
                    ChangeState(State.Order);
                break;
            case State.Order:
                if (IsReceiveMyOrder())
                    ChangeState(State.Eat);
                break;
            case State.Eat:
                if (IsFoodDisappear())
                    ChangeState(State.Exit);
                break;
            case State.Exit:
                if (IsNearExit())
                    Clear();
                break;
        }

        fsm.UpdateState();
    }

    void ChangeState(State nextState)
    {
        curState = nextState;

        switch (curState)
        {
            case State.Sit:
                fsm.ChangeState(new SitState(this));
                break;
            case State.Order:
                fsm.ChangeState(new OrderState(this));
                break;
            case State.Eat:
                fsm.ChangeState(new EatState(this));
                break;
            case State.Exit:
                fsm.ChangeState(new ExitState(this));
                break;
        }
    }

    MyEnum.FoodType ChoiceMyOrder()
    {
        int randNum = Random.Range(0, GameManager.Data.foodCount);

        return (MyEnum.FoodType)randNum;
    }

    bool IsNearSeat()
    {
        return Vector2.Distance(transform.position, mySeat.transform.position) < 0.1f;
    }

    bool IsReceiveMyOrder()
    {
        if (myOrder == MyEnum.FoodType.None)
            return false;

        return mySeat.MyFoodType == myOrder;
    }

    bool IsFoodDisappear()
    {
        return mySeat.ReadiedFood == null;
    }

    bool IsNearExit()
    {
        return Vector2.Distance(transform.position, exit.position) < 0.1f;
    }

    void Clear()
    {
        Debug.Log("Clear!");
        Destroy(gameObject);
    }
}

public class SitState : State
{
    public SitState(Customer customer)
    {
        this.customer = customer;
    }

    public override void OnStateEnter()
    {
        customer.transform.position = customer.exit.transform.position;
    }

    public override void OnStateUpdate()
    {
        MoveToSeat();
    }
    
    public override void OnStateExit()
    {
        customer.mySeat.Sit(customer.transform.gameObject);
    }


    void MoveToSeat()
    {
        customer.transform.position = Vector2.MoveTowards(customer.transform.position, customer.mySeat.transform.position, customer.speed);
    }
}

public class OrderState : State
{
    public OrderState(Customer customer)
    {
        this.customer = customer;
    }

    public override void OnStateEnter()
    {
        customer.orderFood.Init(customer.MyOrder);
        customer.orderBubble.SetActive(true);
        customer.orderBubbleText.text = customer.orderFood.myFood.FoodName;
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit()
    {
        customer.orderBubble.SetActive(false);
    }
}

public class EatState : State
{
    public EatState(Customer customer)
    {
        this.customer = customer;
    }

    public override void OnStateEnter()
    {
        customer.mySeat.ReadiedFood.Disappear();
    }

    public override void OnStateUpdate()
    {
    }

    public override void OnStateExit()
    {
    }
}

public class ExitState : State
{
    public ExitState(Customer customer)
    {
        this.customer = customer;
    }

    public override void OnStateEnter()
    {
    }

    public override void OnStateUpdate()
    {
        customer.transform.position = Vector2.MoveTowards(customer.transform.position, customer.exit.position, customer.speed);
    }

    public override void OnStateExit()
    {
    }

}
