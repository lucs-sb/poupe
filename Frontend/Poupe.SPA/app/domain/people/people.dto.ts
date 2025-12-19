import type { People } from "./people.type";


export type PeopleSummaryDTO = {
  users: People[];
  totalIncomes: number;
  totalExpenses: number;
  netBalance: number;
};

export type CreatePeopleDTO = { name: string; age: number };