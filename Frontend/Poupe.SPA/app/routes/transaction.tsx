import { TransactionApi } from "~/integration/transaction.api";
import TransactionPage from "~/pages/transaction/transaction";
import type { Route } from "./+types/transaction";

export async function clientLoader() {
  return await TransactionApi.list();
}

export async function clientAction({ request }: any) {
  if (request.method === "POST") {
    const formData = await request.formData();
    const description = formData.get("description") as string;
    const value = Number(formData.get("value"));
    const type = formData.get("type") as string;
    const categoryId = formData.get("categoryId") as string;
    const userId = formData.get("userId") as string;
    await TransactionApi.create({ description: description, value: value, type: type, categoryId: categoryId, userId: userId });
  }

  if (request.method === "DELETE") {
    const formData = await request.formData();
    const id = formData.get("id") as string;
    await TransactionApi.remove(id);
  }
}

export default function Transaction({ loaderData }: Route.ComponentProps) {
  return TransactionPage(loaderData);
}
