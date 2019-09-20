using UnityEngine;
using System.Collections;

public class TextTexelAnimator_GPU : MonoBehaviour
{
    public Vector2 resolution = new Vector2(1920, 1080);
    public int cellSize = 6;
    public int pixelPitch = 36;
    public Texture2D sprite;
    public RenderTexture sourceTexture;
//    public RenderTexture sourceTexture02;
    public Mesh instanceMesh;
    int instanceCount = 0;
    public Material instanceMaterial;
    public int subMeshIndex = 0;

    private int cachedInstanceCount = -1;
    private int cachedSubMeshIndex = -1;
    private ComputeBuffer positionBuffer;
    private ComputeBuffer UVBuffer;
    private ComputeBuffer argsBuffer;
    private uint[] args = new uint[5] { 0, 0, 0, 0, 0 };

    void Start()
    {
        argsBuffer = new ComputeBuffer(1, args.Length * sizeof(uint), ComputeBufferType.IndirectArguments);
        UpdateBuffers();
    }

    void Update()
    {
        // Update starting position buffer
        if (cachedInstanceCount != instanceCount || cachedSubMeshIndex != subMeshIndex)
            UpdateBuffers();
        

        // Render
        Graphics.DrawMeshInstancedIndirect(instanceMesh, subMeshIndex, instanceMaterial, new Bounds(Vector3.zero, new Vector3(10000.0f, 10000.0f, 100.0f)), argsBuffer);
    }
    void UpdateBuffers()
    {
        // Ensure submesh index is in range
        if (instanceMesh != null)
            subMeshIndex = Mathf.Clamp(subMeshIndex, 0, instanceMesh.subMeshCount - 1);

        //共通のプロパティ
        instanceMaterial.SetTexture("_MainTex", sprite);
        instanceMaterial.SetInt("_FrameCount", 0);
        instanceMaterial.SetTexture("_SourceTex", sourceTexture);
//        instanceMaterial.SetTexture("_SourceTex02", sourceTexture02);
        instanceMaterial.SetFloat("_CellSize", cellSize);
        instanceMaterial.SetFloat("_QuadScale", pixelPitch);

        // Positions
        if (positionBuffer != null) positionBuffer.Release();
        if (UVBuffer != null) UVBuffer.Release();
        
        for (int x = 0; x < resolution.x; x += pixelPitch)
        {
            for (int y = 0; y < resolution.y; y += pixelPitch)
            {
                this.instanceCount++;
            }
        }

        positionBuffer = new ComputeBuffer(instanceCount, 12);
        UVBuffer = new ComputeBuffer(instanceCount, 8);
        Vector3[] positions = new Vector3[instanceCount];
        float[] grays = new float[instanceCount];
        Vector2[] UVs = new Vector2[instanceCount];
        
        var offsetPos = new Vector3(-resolution.x / 2f + pixelPitch / 2f,  -resolution.y / 2f + pixelPitch / 2f, 0) + transform.position;
        int count = 0;
        for (int x = 0; x < resolution.x; x += pixelPitch)
        {
            for (int y = 0; y < resolution.y; y += pixelPitch)
            {
                positions[count] = new Vector3(x,y,0) + offsetPos;
                UVs[count] = new Vector2(x/resolution.x, y/resolution.y);
                count++;
            }
        }
        positionBuffer.SetData(positions);
        UVBuffer.SetData(UVs);
        instanceMaterial.SetBuffer("_PositionBuffer", positionBuffer);
        instanceMaterial.SetBuffer("_UVBuffer", UVBuffer);
        // Indirect args
        if (instanceMesh != null)
        {
            args[0] = (uint)instanceMesh.GetIndexCount(subMeshIndex);
            args[1] = (uint)instanceCount;
            args[2] = (uint)instanceMesh.GetIndexStart(subMeshIndex);
            args[3] = (uint)instanceMesh.GetBaseVertex(subMeshIndex);
        }
        else
        {
            args[0] = args[1] = args[2] = args[3] = 0;
        }
        argsBuffer.SetData(args);

        cachedInstanceCount = instanceCount;
        cachedSubMeshIndex = subMeshIndex;
    }

    void OnDisable()
    {
        if (positionBuffer != null)
            positionBuffer.Release();
        positionBuffer = null;

        if (UVBuffer != null)
            UVBuffer.Release();
        UVBuffer = null;

        if (argsBuffer != null)
            argsBuffer.Release();
        argsBuffer = null;
    }
}