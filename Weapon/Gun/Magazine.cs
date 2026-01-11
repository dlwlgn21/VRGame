using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.XR;
using UnityEngine.XR.Interaction.Toolkit;
public class Magazine : MonoBehaviour, IReloadable
{
    public UnityEvent OnReloadStartEvent;
    public UnityEvent OnReloadEndEvent;
    public UnityEvent<int, int> OnBulletChagnedEvent;
    public UnityEvent<float> OnChargeChagnedEvent;
    // =========================
    // XR Reload Gesture Settings
    // =========================
    [Header("XR Reload Gesture")]
    [SerializeField] private XRNode _reloadControllerNode = XRNode.RightHand;
    [SerializeField] private float _reloadVelocityThreshold = 1.2f; // 아래 방향 속도 임계값
    [SerializeField] private float _reloadCooldown = 1.0f;

    // XR Device Simulator Fallback
    [Header("XR Simulator Fallback")]
    [SerializeField] private Transform _simulatedRightHandTransform;

    // Grab Gate
    [Header("Grab Gate")]
    [SerializeField] private XRGrabInteractable _grabInteractable;

    // =========================
    // Magazine Settings
    // =========================
    [SerializeField] private int _maxBullet = 20;
    [SerializeField] private float _chargingTimeInSec = 2f;

    // =========================
    // State
    // =========================
    private int _currBullet;
    private bool _isReloading;
    private bool _isGrabbed;
    private float _lastReloadTriggerTime;

    // Simulator velocity cache
    private Vector3 _prevHandPos;
    private bool _hasPrevHandPos;

    // Cached InputDevice (실기기)
    private InputDevice _reloadDevice;

    public int CurrentBullet
    {
        get { return _currBullet; }
        set
        {
            _currBullet = Mathf.Clamp(value, 0, _maxBullet);
            OnBulletChagnedEvent?.Invoke(_currBullet, _maxBullet);
            OnChargeChagnedEvent?.Invoke((float)_currBullet / _maxBullet);
        }
    }

    // =========================
    // Unity Lifecycle
    // =========================
    private void Awake()
    {
        if (_grabInteractable == null)
        {
            _grabInteractable = GetComponent<XRGrabInteractable>();
        }
    }

    private void OnEnable()
    {
        if (_grabInteractable != null)
        {
            _grabInteractable.selectEntered.AddListener(OnGrabEntered);
            _grabInteractable.selectExited.AddListener(OnGrabExited);
        }

        TryInitializeDevice();
        InputDevices.deviceConnected += OnDeviceConnected;
    }

    private void OnDisable()
    {
        if (_grabInteractable != null)
        {
            _grabInteractable.selectEntered.RemoveListener(OnGrabEntered);
            _grabInteractable.selectExited.RemoveListener(OnGrabExited);
        }

        InputDevices.deviceConnected -= OnDeviceConnected;
    }

    private void Start()
    {
        CurrentBullet = _maxBullet;
    }

    private void Update()
    {
        CacheSimulatedHandPosition();
        CheckReloadGesture();
    }

    // =========================
    // Grab Gate
    // =========================
    private void OnGrabEntered(SelectEnterEventArgs args)
    {
        _isGrabbed = true;
    }

    private void OnGrabExited(SelectExitEventArgs args)
    {
        _isGrabbed = false;
    }

    // =========================
    // Shooting
    // =========================
    public bool TryUseBullet(int amount = 1)
    {
        if (CurrentBullet >= amount)
        {
            CurrentBullet -= amount;
            return true;
        }
        return false;
    }

    // =========================
    // Reload Control
    // =========================
    public void StartReload()
    {
        if (_isReloading)
        {
            return;
        }

        _isReloading = true;
        StopAllCoroutines();
        StartCoroutine(StartReload_Co());
    }

    public void StopReload()
    {
        _isReloading = false;
        StopAllCoroutines();
    }

    // =========================
    // Reload Gesture
    // =========================
    private void TryInitializeDevice()
    {
        _reloadDevice = InputDevices.GetDeviceAtXRNode(_reloadControllerNode);
    }

    private void OnDeviceConnected(InputDevice device)
    {
        if (device.characteristics.HasFlag(InputDeviceCharacteristics.Controller) &&
            ((_reloadControllerNode == XRNode.RightHand && device.characteristics.HasFlag(InputDeviceCharacteristics.Right)) ||
             (_reloadControllerNode == XRNode.LeftHand && device.characteristics.HasFlag(InputDeviceCharacteristics.Left))))
        {
            _reloadDevice = device;
        }
    }

    private void CacheSimulatedHandPosition()
    {
        if (_simulatedRightHandTransform == null)
        {
            return;
        }

        if (!_hasPrevHandPos)
        {
            _prevHandPos = _simulatedRightHandTransform.position;
            _hasPrevHandPos = true;
            return;
        }
    }

    private void CheckReloadGesture()
    {
        // 1) 총을 잡고 있을 때만
        if (!_isGrabbed)
        {
            return;
        }

        // 2) 재장전 중 차단
        if (_isReloading)
        {
            return;
        }

        // 3) 쿨다운
        if (Time.time - _lastReloadTriggerTime < _reloadCooldown)
        {
            return;
        }

        // 4) 실기기 우선 (deviceVelocity)
        if (_reloadDevice.isValid &&
            _reloadDevice.TryGetFeatureValue(CommonUsages.deviceVelocity, out Vector3 hwVelocity))
        {
            if (hwVelocity.y < -_reloadVelocityThreshold)
            {
                _lastReloadTriggerTime = Time.time;
                StartReload();
            }
            return;
        }

        // 5) XR Device Simulator Fallback (Transform 기반 속도)
        if (_simulatedRightHandTransform == null || !_hasPrevHandPos)
        {
            return;
        }

        Vector3 currPos = _simulatedRightHandTransform.position;
        Vector3 simVelocity = (currPos - _prevHandPos) / Mathf.Max(Time.deltaTime, 0.0001f);

        if (simVelocity.y < -_reloadVelocityThreshold)
        {
            _lastReloadTriggerTime = Time.time;
            StartReload();
        }

        _prevHandPos = currPos;
    }

    // =========================
    // Reload Coroutine
    // =========================
    private IEnumerator StartReload_Co()
    {
        OnReloadStartEvent?.Invoke();

        float beginTime = Time.time;
        int beginBullet = CurrentBullet;

        float enoughPercentage = 1f - ((float)CurrentBullet / _maxBullet);
        float enoughChargingTime = _chargingTimeInSec * Mathf.Max(enoughPercentage, 0.0001f);

        while (true)
        {
            float t = (Time.time - beginTime) / enoughChargingTime;
            if (t >= 1f)
            {
                break;
            }

            CurrentBullet = (int)Mathf.Lerp(beginBullet, _maxBullet, t);
            yield return null;
        }

        CurrentBullet = _maxBullet;
        _isReloading = false;
        OnReloadEndEvent?.Invoke();
    }
}
