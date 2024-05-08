
/-
  Timp de lucru: 1 ora.
  La final, incarcati un fisier lean denumit `Nume_Prenume_Grupa.lean` la adresa https://www.dropbox.com/request/pwPjc4Z7OS5uxbA5c63W
-/

section

  /-
    **Exercitiul 1: Definiti, prin recursie structurala,
    functia `nthSquareSum` astfel incat
    `nthSquareSum n` calculeaza sum patratelor numerelor naturale de la `0` la `n`.
  -/

  def nthSquareSum (n : Nat) : Nat :=
    match n with
    | Nat.zero => Nat.zero
    | Nat.succ n' => Nat.succ n' * Nat.succ n' + nthSquareSum n'

  def nthSquareSum' (n : Nat) : Nat :=
    match n with
      | 0 => 0
      | n + 1 => (n + 1) * (n + 1) + nthSquareSum' n
end


#eval nthSquareSum 0 -- 0
#eval nthSquareSum 1 -- 1
#eval nthSquareSum 2 -- 5
/-
  Demonstrati urmatoarea teorema.
-/
section
variable (p q r : Prop)
theorem ex2 : p ∧ (q ∧ r) → (p ∧ r) ∧ q := by
  intros hpqr
  have hp := hpqr.left
  have hqr := hpqr.right
  cases hqr with
  | _ hq hr =>
  apply And.intro
  case left =>
    apply And.intro
    case left => assumption
    case right => assumption
  case right => assumption
end

/-
  Demonstrati urmatoarea teorema.
-/
theorem dne {p : Prop} : ¬¬p → p := by
  intro hnnp
  apply Or.elim (Classical.em p)
  . intro hp
    exact hp
  . intro hnp
    exact absurd hnp hnnp

section
variable {α : Type} (p : α → Prop)

theorem ex3 : (∀ x, p x) → (∀ x, (¬¬p x)) := by
  intros hp hx
  apply Classical.byContradiction
  intros hnnnp
  have hnp := dne hnnnp
  specialize hp hx
  contradiction

end


-- *Examen - Elemente de securitate si logica aplicata*

macro "use" e:term : tactic => `(tactic| refine Exists.intro $e ?_)

-- Subiectul 1 [1p]
-- a. Definiti, prin recursie structurala, urmatoarea functie, pentru orice x si y numere naturale:

-- my_func(x, y) := 2 * y                                 daca x = 0
--                  3 * (x + y) + 2 * my_func(x - 1, y)   altfel


-- b. Evaluati functia in x = 3 si y = 5.

def my_func (x y : Nat) : Nat :=
  match x with
  | 0 => 2 * y
  | x + 1 => 2 * (my_func x y) + 3 * (x + 1 + y)

#eval my_func 3 5



-- Subiectul 2 [1p]
-- Demonstrati teorema din logica propozitionala: (p → r) ∧ (q → ¬r) → ¬(p ∧ q).
-- Puteti folosi regulile de eliminare, respectiv introducere a dublei negatii.

variable { p q r : Prop }

theorem dni : p → ¬¬p := by
  intro hp
  apply Classical.byContradiction
  intro hnnnp
  have hnp := dne hnnnp
  contradiction

example : (p → r) ∧ (q → ¬r) → ¬(p ∧ q) := by
  intros hpqr
  have hpr := And.left hpqr
  have hqr := And.right hpqr
  apply Classical.byContradiction
  intro hnnpq
  have hpq := dne hnnpq
  have hp := And.left hpq
  have hq := And.right hpq
  have hr := hpr hp
  have hnr := hqr hq
  contradiction


-- Subiectul 3 [1p]
-- Demonstrati urmatoarea teorema in logica de ordinul I: ∀x (p(x) → q(x)) ∧ ∀x p(x) → q(a).
-- Aveti date predicatele p si q, de tipul α → Prop, si o variabila a : α.

variable {p q : α → Prop}
variable {a : α}

example : (∀ x : α, p x → q x) ∧ (∀ x, p x) → q a := by
  intros h
  have hpq := And.left h
  have hp := And.right h
  specialize hpq a
  specialize hp a
  have hq := hpq hp
  exact hq
