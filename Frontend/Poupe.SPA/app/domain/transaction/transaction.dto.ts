export type CreateTransactionDTO = {
  description: string; 
  value: number; 
  type: string;
  categoryId: string;
  userId: string;
};