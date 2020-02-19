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
  import CreateConsultations from "./CreateConsultations";
  import EditConsultations from "./EditConsultations";
  import DeleteConsultations from "./DeleteConsultations";

  export default {
    name: "Consultations",
    components: {CreateConsultations, EditConsultations, DeleteConsultations},
    data() {
      return {
        currentOpportunities: null,
        opportunities: [
          {
            component: 'CreateConsultations',
            tab: 'Добавление консультации'
          },
          {
            component: 'EditConsultations',
            tab: 'Редактирование консультации'
          },
          {
            component: 'DeleteConsultations',
            tab: 'Удаление консультации'
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

</style>