using UnityEngine;
using System.Collections;

public class Pair<T, U> : System.IEquatable<Pair<T, U>> {
    public T fst;
    public U snd;

    public Pair() {}
    public Pair(T fst, U snd) {
        this.fst = fst;
        this.snd = snd;
    }
    public Pair(Pair<T, U> rhs) {
        this.fst = rhs.fst;
        this.snd = rhs.snd;
    }

    public bool Equals(Pair<T, U> rhs) {
        return Equals(fst, rhs.fst) && Equals(snd, rhs.snd);
    }

    public static bool operator == (Pair<T, U> lhs, Pair<T, U> rhs) {
        return lhs.Equals(rhs);
    }

    public static bool operator != (Pair<T, U> lhs, Pair<T, U> rhs) {
        return !(lhs == rhs);
    }
};
