import type { Route } from "./+types/home";
import PeoplePage from "~/pages/people/people";
import { PeopleApi } from "~/integration/people.api";

export function meta({}: Route.MetaArgs) {
  return [
    { title: "Poupe App" },
    { name: "description", content: "Controle de Gastos Residenciais" },
  ];
}

export async function clientLoader() {
  return await PeopleApi.list();
}

export async function clientAction({ request }: any) {
  if (request.method === "POST") {
    const formData = await request.formData();
    const name = formData.get("name") as string;
    const age = Number(formData.get("age"));
    await PeopleApi.create({ name: name, age: age });
  }

  if (request.method === "DELETE") {
    const formData = await request.formData();
    const id = formData.get("id") as string;
    await PeopleApi.remove(id);
  }
}


export default function Home({ loaderData }: Route.ComponentProps) {
  return PeoplePage(loaderData);
}
