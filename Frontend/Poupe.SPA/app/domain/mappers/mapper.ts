import type { CategoryPurpose } from "../category/purpose.enum";
import type { TransactionType } from "../transaction/transactionType.enum";

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

export function translateTransactionType(type: TransactionType): string {
  switch (type) {
    case "Income":
      return "Receita";
    case "Expense":
      return "Despesa";
    default:
      return "—";
  }
}