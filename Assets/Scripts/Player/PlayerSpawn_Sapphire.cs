using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerSpawn_Sapphire : MonoBehaviour
{
    [SerializeField] private GameObject playerprefab;//�v���C���[�̃v���n�u
    [SerializeField] private BulletPool bulletPool;//�e�̃v�[��
    [SerializeField] private float radius = 3.0f;//�v���C���[�̐������a
    private int playersize = 4;//�v���C���[�̐�
    private void Awake()
    {
        for (int i = 0; i < playersize; i++)
        {
            //�X�|�[���G���A���͈͓̔��̃����_���Ȉʒu���擾
            Vector2 randompos = Random.insideUnitCircle * radius;
            Vector3 spawnpos = new Vector3(randompos.x, randompos.y, 0) + transform.position;

            //�v���C���[�̐���
            GameObject instance = Instantiate(playerprefab, spawnpos, transform.rotation,transform);
            instance.name = $"{playerprefab.name}_{i}";

            //���[�J���X�P�[�������Z�b�g
            instance.transform.localScale = Vector3.one;

            // ���[���h�X�P�[�������Z�b�g
            instance.transform.SetParent(null, true);
            instance.transform.localScale = playerprefab.transform.localScale;
            instance.transform.SetParent(transform, true);

            //�v���C���[�̕ϐ��̐ݒ�
            PlayerControl playerControl = instance.GetComponent<PlayerControl>();
            playerControl.team = PlayerControl.Team.Sapphire;
            playerControl.isPlayer = false;

            //�v���C���[�𐶐�������ɒe�̃v�[�����Z�b�g
            if (playerControl != null)
            {
                playerControl.SetBulletPool(bulletPool);
            }
            else
            {
                Debug.LogError("PlayerControl�R���|�[�l���g��������܂���B");
            }
        }
    }
}
