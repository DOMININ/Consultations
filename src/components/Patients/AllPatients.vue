<template>
  <div>
    <section class="search">
      <input
        type="text"
        placeholder="Введите фамилию для поиска"
        class="input-field input-field__name"
        v-model="firstNameSearchQuery"
      >
      <input
        type="text"
        placeholder="Введите СНИЛС для поиска"
        class="input-field input-field__name"
        v-model="snilsSearchQuery"
      >
    </section>
    <table class="all">
      <thead>
        <tr>
          <th>Фамилия</th>
          <th>Имя</th>
          <th>Отчество</th>
          <th>Дата рождения</th>
          <th>Пол</th>
          <th>СНИЛС</th>
        </tr>
      </thead>
      <tbody>
        <tr v-for="(info, id) in this.resultQuery()" :key="id" class="row">
          <td class="all-info-item">{{ info.firstName }}</td>
          <td class="all-info-item">{{ info.lastName }}</td>
          <td class="all-info-item">{{ info.middleName }}</td>
          <td class="all-info-item">{{ convertDate(info.birthDate) }}</td>
          <td class="all-info-item">{{ info.gender }}</td>
          <td class="all-info-item">{{ info.snils }}</td>
        </tr>
      </tbody>
    </table>
  </div>
</template>

<script>
  import axios from "axios";
  import moment from 'moment';

  export default {
    name: "AllPatients",
    data() {
      return {
        inform: null,
        firstNameSearchQuery: '',
        snilsSearchQuery: '',
      }
    },
    methods: {
      convertDate(date) {
        return moment(date).format('DD.MM.YYYY');
      },
      resultQuery() {
        if (this.firstNameSearchQuery || this.snilsSearchQuery) {
          return this.inform
            .filter(patient => {
              return (
                patient.firstName.startsWith(this.firstNameSearchQuery) &&
                patient.snils.startsWith(this.snilsSearchQuery)
              );
            });
        } else {
          return this.inform;
        }
      }
    },
    mounted() {
      axios
        .get('https://localhost:5001/api/patient')
        .then(response => (this.inform = response.data));
    },
  }
</script>

<style lang="scss">
  .search {
    display: flex;
    justify-content: space-between;
  }

  .all {
    width: 100%;
    border-collapse: collapse;
    text-align: center;

    th,
    td {
      border: 1px solid #c2c2c2;
      padding: 5px 10px;
    }

    thead tr {
      background-color: #66d1c6;
      color: white;
    }
  }

  .row:nth-child(even) {
    background-color: #e5e5e5;
  }

  .row:hover {
    background: #66d1c6;
    transition-duration: 0.5s;
  }
</style>