import { useEffect, useState } from "react";
import type { Category } from "~/domain/category/category.type";
import { CategoryApi } from "~/integration/category.api";

export function useCategoriesData() {
  const [categories, setCategories] = useState<Category[]>([]);
  const [loaded, setLoaded] = useState(false);

  useEffect(() => {
    if (!loaded) {
      CategoryApi.list().then((data) => {
        setCategories(data ?? []);
        setLoaded(true);
      });
    }
  }, [loaded]);

  return { categories, loaded };
}