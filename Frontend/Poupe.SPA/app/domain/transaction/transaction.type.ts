import type { Category } from "../category/category.type";
import type { People } from "../people/people.type";
import type { TransactionType } from "./transactionType.enum";

export type Transaction = {
  id: string; 
  description: string; 
  value: number; 
  type: TransactionType;
  category: Category; 
  people: People; 
};