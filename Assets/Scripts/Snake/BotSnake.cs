﻿using SnakeGame.Assets.Scripts;
using UnityEngine;

namespace Assets.Scripts.Snake
{
    public class BotSnake : SnakeScript
    {
        [SerializeReference] private BlockSpawner _blockSpawner;
        [SerializeField] private int _checkDirectionTime = 0;

        private int _moveInteractions = 0;

        protected override void MoveSnake()
        {
            var blocks = _blockSpawner.GetBlocksSpawned();
            if (blocks.Count > 0 && _moveInteractions > _checkDirectionTime)
            {
                var block = blocks[0];
                var desiredDirection = (block.transform.position - _snakeHead.transform.position).normalized;
                var currentDirection = (_snakeHead.transform.position - _snakeHead.LastPostion).normalized;
                UpdateBotDirection(currentDirection, desiredDirection);
                _moveInteractions = 0;
            }

            _moveInteractions++;

            //update before call base
            base.MoveSnake();
        }


        private void UpdateBotDirection(Vector3 dirA, Vector3 dirB)
        {
            Vector3 cross = Vector3.Cross(dirA, dirB);
            var mod = cross.y;

            var dot = Vector3.Dot(dirA, dirB);
            if (dot > 0.9f)
            {
                return;
            }

            if (mod > 0.0)
            {
                _movementDirection = Quaternion.Euler(0, 90, 0) * _movementDirection;
            }
            else
            {
                _movementDirection = Quaternion.Euler(0, -90, 0) * _movementDirection;
            }
        }
    }
}