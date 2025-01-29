import type { Book } from "@/models/book"
import axios from "axios"
import { defineStore } from "pinia"
import { ref } from "vue"

export const useBookStore = defineStore('books', () => {
  const books = ref<Book[]>([])

  async function getOrRefreshBooks(): Promise<void> {
    try {
      const response = await axios.get<Book[]>('/api/books');

      if (response.status === 200) {
        books.value = response.data;
      }
      else {
        console.error(response.statusText);
      }
    } catch (error) {
      console.error(error);
    }
  }


  return {
    books,
    getOrRefreshBooks
  }
})