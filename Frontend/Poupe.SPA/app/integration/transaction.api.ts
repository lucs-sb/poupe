import { http } from "./http";
import type { CreateTransactionDTO } from "~/domain/transaction/transaction.dto";
import type { Transaction } from "~/domain/transaction/transaction.type";

export const TransactionApi = {
  list: () => http<Transaction[]>("/transaction"),
  create: (dto: CreateTransactionDTO) =>
    http<Transaction>("/transaction", { method: "POST", body: JSON.stringify(dto) }),
  remove: (id: string) =>
    http<void>(`/transaction/${id}`, { method: "DELETE" }),
};