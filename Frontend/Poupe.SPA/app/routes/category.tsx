import { CategoryApi } from "~/integration/category.api";
import CategoryPage from "~/pages/category/category";
import type { Route } from "./+types/category";

export async function clientLoader() {
  return await CategoryApi.list();
}

export async function clientAction({ request }: any) {
  if (request.method === "POST") {
    const formData = await request.formData();
    const description = formData.get("description") as string;
    const purpose = formData.get("purpose") as string;
    await CategoryApi.create({ description: description, purpose: purpose });
  }

  if (request.method === "DELETE") {
    const formData = await request.formData();
    const id = formData.get("id") as string;
    await CategoryApi.remove(id);
  }
}

export default function Category({ loaderData }: Route.ComponentProps) {
  return CategoryPage(loaderData);
}