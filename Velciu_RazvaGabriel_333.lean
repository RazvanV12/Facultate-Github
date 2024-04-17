
/-
  Timp de lucru: 1 ora.
  La final, incarcati un fisier lean denumit `Nume_Prenume_Grupa.lean` la adresa https://www.dropbox.com/request/UagLsQbkdC6miELtFcLp
-/

section

  /-
    **Exercitiul 1: Definiti, prin recursie structurala,
    functie `sumInterval` astfel incat
    `sumInterval n m` calculeaza suma numerelor naturale din intervalul `m, ..., m + n` (inclusiv).
  -/

  def sumInterval (n : Nat) (m : Nat) : Nat :=
    match n with
    | 0 => m
    | n + 1 => m + n + 1 + sumInterval n m

#eval sumInterval 2 3 -- 3 + 3 + 1 + 3 + 2 = 12
#eval sumInterval 2 1 -- 1 + 1 + 1 + 1 + 2 = 6
#eval sumInterval 1 5 -- 5 + 5 + 1 = 11

end

theorem dne {p : Prop} : ¬¬p → p := by
  intro hnnp
  apply Or.elim (Classical.em p)
  . intro hp
    exact hp
  . intro hnp
    exact absurd hnp hnnp

theorem dni : p → ¬¬p := by
intros hp
apply Classical.byContradiction
intros hnnnp
have hnp := dne hnnnp
exact absurd hp hnp
/-
  Demonstrati urmatoarea teorema.
-/
section
variable (p q r : Prop)
theorem ex2 : p ∧ q → q ∧ ¬¬p := by
  intros hpq
  have hp := hpq.left
  have hq := hpq.right
  apply And.intro
  case left => assumption
  case right =>
    have hnnp := dni hp
    assumption
end

/-
  Demonstrati urmatoarea teorema
-/
section
variable {α : Type} (p : α → Prop) (q : α → Prop)

theorem ex3 : (∀ x, p x) → (∀ x, p x ∨ q x) := by
  intros hp x
  specialize hp x
  apply Or.inl
  assumption
end
