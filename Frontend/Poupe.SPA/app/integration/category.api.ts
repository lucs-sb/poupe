import { http } from "./http";
import type { CreateCategoryDTO } from "~/domain/category/category.dto";
import type { Category } from "~/domain/category/category.type";

export const CategoryApi = {
  list: () => http<Category[]>("/category"),
  create: (dto: CreateCategoryDTO) =>
    http<Category>("/category", { method: "POST", body: JSON.stringify(dto) }),
  remove: (id: string) =>
    http<void>(`/category/${id}`, { method: "DELETE" }),
};