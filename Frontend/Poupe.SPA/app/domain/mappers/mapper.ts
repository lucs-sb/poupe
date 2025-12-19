import type { CategoryPurpose } from "../category/purpose.enum";

export function translatePurpose(purpose: CategoryPurpose): string {
  switch (purpose) {
    case "Income":
      return "Receita";
    case "Expense":
      return "Despesa";
    case "Both":
      return "Ambas";
    default:
      return "—";
  }
}