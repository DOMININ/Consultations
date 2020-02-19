<template>
  <div class="patients">
    <ul class="patients-list">
      <li
        v-for="opportunity in opportunities"
        v-bind:key="opportunity.component"
      >
        <button
          class="patients-item"
          v-bind:class="{ active: currentOpportunities === opportunity.component }"
          v-on:click="currentOpportunities = opportunity.component"
        >
          {{ opportunity.tab }}
        </button>
      </li>
    </ul>
    <section class="empty">
      <div
        v-if="currentOpportunities"
      >
        <component v-bind:is="selectedOpportunityComponent" />
      </div>
      <p class="title" v-else>
        Выберите нужную Вам вкладку
      </p>
    </section>
  </div>
</template>

<script>

  import AllPatients from "./AllPatients";
  import CreatePatients from "./CreatePatients";
  import EditPatients from "./EditPatients";
  import DeletePatients from "./DeletePatients";
  import ConsultPatients from "./ConsultPatients";

  export default {
    name: "Patient",
    components: {AllPatients, CreatePatients, EditPatients, ConsultPatients, DeletePatients},
    data() {
      return {
        currentOpportunities: null,
        opportunities: [
          {
            component: 'AllPatients',
            tab: 'Все пациенты'
          },
          {
            component: 'CreatePatients',
            tab: 'Добавление пациента'
          },
          {
            component: 'EditPatients',
            tab: 'Редактирование пациента'
          },
          {
            component: 'ConsultPatients',
            tab: 'Консультации пациента'
          },
          {
            component: 'DeletePatients',
            tab: 'Удаление пациента'
          }
        ]
      }
    },
    computed: {
      selectedOpportunityComponent: function() {
        return this.currentOpportunities;
      }
    },
  }
</script>

<style lang="scss">
  .patients {
    display: flex;
    flex-grow: 1;
    padding-top: 40px;
    font-size: 18px;

    &-list {
      display: flex;
      flex-direction: column;
      justify-content: space-between;
      margin: 0;
      padding: 0 15px 0 0;
      list-style: none;
      min-width: 185px;
      max-width: 185px;
      border-right: 1px solid #e5e5e5;
     }

    &-item {
      padding: 10px;
      width: 100%;
      font-size: 19px;
      text-align: start;
      border: 1px solid #c2c2c2;
      border-radius: 7px;

      &:hover {
        color: #fff;
        background-color: #66d1c6;
        border-radius: 7px;
        cursor: pointer;
      }
    }
  }

  .active {
    color: #fff;
    background-color: #66d1c6;
    border-radius: 7px;
  }

  .empty {
    position: relative;
    flex-grow: 1;
    padding-left: 20px;

    & .title {
      position: absolute;
      top: 130px;
      margin: 0;
      text-align: center;
      font-size: 35px;
    }
  }
</style>