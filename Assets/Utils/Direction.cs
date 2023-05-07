using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace BuilderGame.Utils {
    public struct Direction {
        private int _internalValue { get; set; }

        public static readonly int Right = 0;
        public static readonly int Up = 1;
        public static readonly int Left = 2;
        public static readonly int Down = 3;
        public static readonly int Invalid = 4;

        public Direction(int value) {
            _internalValue = value;
        }
        public Direction(DirectionEnum enumValue) {
            _internalValue = (int)enumValue;
        }

        public override bool Equals(object obj) {
            if (obj == null) {
                return false;
            }

            Direction other = new Direction(Direction.Invalid);

            if (obj.GetType() == typeof(int)) {
                other = new Direction((int)obj);
            }
            if (obj.GetType() == typeof(DirectionEnum)) {
                other = new Direction((DirectionEnum)obj);
            }
            if (obj.GetType() == typeof(Direction)) {
                other = (Direction)obj;
            }

            return this._internalValue == other._internalValue;
        }

        public override int GetHashCode()
        {
            return _internalValue.GetHashCode();
        }

        public static implicit operator int(Direction d)  {
            return d._internalValue;
        }

        public static implicit operator Vector3(Direction d)  {
            switch (d._internalValue) {
                case 0:
                    return Vector3.right;
                case 1:
                    return Vector3.up;
                case 2:
                    return Vector3.left;
                case 3:
                    return Vector3.down;
                default:
                    return Vector3.zero;
            }
        }
        public static implicit operator Vector3Int(Direction d)  {
            switch (d._internalValue) {
                case 0:
                    return Vector3Int.right;
                case 1:
                    return Vector3Int.up;
                case 2:
                    return Vector3Int.left;
                case 3:
                    return Vector3Int.down;
                default:
                    return Vector3Int.zero;
            }
        }
        
        public static implicit operator bool(Direction d)
        {
            return d._internalValue >= 0 && d._internalValue < 4;
        }

        public static implicit operator Direction(int value)
        {
            return new Direction(value);
        }

        public static implicit operator Direction(DirectionEnum value)
        {
            return new Direction(value);
        }

        public static Direction operator +(Direction first, Direction second)
        {
            if (first._internalValue == Invalid || second._internalValue == Invalid)
                return new Direction(Invalid);
            return new Direction((first._internalValue + second._internalValue)%4);
        }

        public static Direction operator -(Direction first, Direction second)
        {
            if (first._internalValue == Invalid || second._internalValue == Invalid)
                return new Direction(Invalid);
            return new Direction((first._internalValue - second._internalValue + 4)%4);
        }
    }

    public enum DirectionEnum {
        Right,
        Up,
        Left,
        Down
    }
}